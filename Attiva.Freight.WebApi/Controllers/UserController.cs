using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Attiva.Freight.Authorization.Library.DTO.Access;
using Attiva.Freight.WebApi.Configurations;
using Microsoft.Extensions.Caching.Memory;
using Attiva.Freight.Authorization.Application.Interfaces;
using Attiva.Communication;

namespace Attiva.Freight.WebApi.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthorizationFacade _facade;

        public UserController(IAuthorizationFacade facade)
        {
            _facade = facade;
        }

        /// <summary>
        /// A partir de dados de autenticação fornecidos, retorna token de autorização para as rotas da API que fornecem dados de frete
        /// </summary>
        /// <remarks>
        /// Necessário informar id do usuário e senha, ou refresh token obtido anteriormente 
        /// </remarks>
        /// <response code="200">Retorna o valor do token, refreh token e respectivos tempos de expiração em segundos</response>
        /// <response code="400">Quando dados de autentição fornecidos erroneamente</response>
        [Produces("application/json")]
        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public ActionResult<Response<TokenResponseDTO>> Post([FromBody] CredentialsDTO dto,
                                                             [FromServices]SigningConfigurations signingConfigurations,
                                                             [FromServices]TokenConfigurations tokenConfigurations,
                                                             [FromServices]IMemoryCache cache)
        {
            if (dto == null ||
                (string.IsNullOrWhiteSpace(dto.UserID) && string.IsNullOrWhiteSpace(dto.SecretKey) && string.IsNullOrWhiteSpace(dto.RefreshToken)) ||
                (!string.IsNullOrWhiteSpace(dto.UserID) && !string.IsNullOrWhiteSpace(dto.SecretKey) && !string.IsNullOrWhiteSpace(dto.RefreshToken)) ||
                (string.IsNullOrWhiteSpace(dto.RefreshToken) && (string.IsNullOrWhiteSpace(dto.UserID) || string.IsNullOrWhiteSpace(dto.SecretKey))) ||
                (!string.IsNullOrWhiteSpace(dto.RefreshToken) && (!string.IsNullOrWhiteSpace(dto.UserID) || !string.IsNullOrWhiteSpace(dto.SecretKey)))
                )
            {
                return BadRequest();
            }


            var watch = Stopwatch.StartNew();

            Response<TokenResponseDTO> result = new Response<TokenResponseDTO>();

            try
            {
                bool isValidCredentials = false;
                string cachedUserId = null;

                if (string.IsNullOrWhiteSpace(dto.RefreshToken))
                {
                    dto.SecretKey = GetEncode512(dto.SecretKey);
                    isValidCredentials = _facade.Login(dto);
                }

                if (isValidCredentials ||
                    (cache.TryGetValue("refresh_token", out string cachedRefreshToken) && cache.TryGetValue("user_id", out cachedUserId) && cachedRefreshToken == dto.RefreshToken))
                {
                    ClaimsIdentity identity = new ClaimsIdentity(
                        new GenericIdentity(cachedUserId ?? dto.UserID, "Login"),
                        new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, cachedUserId ?? dto.UserID)
                        }
                    );

                    var now = DateTime.Now;
                    var cacheExpirate = now + TimeSpan.FromSeconds(tokenConfigurations.Seconds) * 2;

                    TokenResponseDTO tokenResponse = new TokenResponseDTO()
                    {
                        TokenExpirate = tokenConfigurations.Seconds,
                        RefreshTokenExpirate = tokenConfigurations.Seconds * 2
                    };

                    var handler = new JwtSecurityTokenHandler();
                    var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                    {
                        Issuer = tokenConfigurations.Issuer,
                        Audience = tokenConfigurations.Audience,
                        SigningCredentials = signingConfigurations.SigningCredentials,
                        Subject = identity,
                        NotBefore = now,
                        Expires = now + TimeSpan.FromSeconds(tokenConfigurations.Seconds)
                    });

                    tokenResponse.Token = handler.WriteToken(securityToken);

                    cache.Remove("refresh_token");
                    cache.Remove("user_id");

                    tokenResponse.RefreshToken = SetCache(cache, "refresh_token", Guid.NewGuid().ToString().Replace("-", string.Empty), cacheExpirate);
                    SetCache(cache, "user_id", cachedUserId ?? dto.UserID, cacheExpirate);

                    result.Items.Add(tokenResponse);
                    result.Success = true;
                }
                else
                {
                    result.AddError(!isValidCredentials ? "Invalid credentials" : "Refresh token is invalid or expired");
                }
            }
            catch (Exception ex)
            {
                result.AddError(ex);
            }

            watch.Stop();

            result.ResponseTime = watch.Elapsed.TotalSeconds.ToString("0.0### seconds");
            result.HttpStatusCode = System.Net.HttpStatusCode.OK;

            return result;
        }

        /// <summary>
        /// Realiza alteração da senha do usuário da API.
        /// </summary>
        /// <remarks>
        /// Necessário informar o valor da nova senha.
        /// </remarks>
        /// <response code="200">Alteração da senha realizada com sucesso</response>
        /// <response code="400">Quando não informado o valor da nova senha</response>
        /// <response code="401">Token não informado ou expirado</response>
        [Produces("application/json")]
        [Authorize("Bearer")]
        [HttpPost]
        [Route("reset/secret")]
        public ActionResult<Response<object>> ChangeSecretKey([FromBody] string secretKey,
                                                              [FromServices]IMemoryCache cache)
        {
            if (string.IsNullOrWhiteSpace(secretKey))
            {
                return BadRequest();
            }

            var watch = Stopwatch.StartNew();

            var dto = new CredentialsDTO()
            {
                SecretKey = GetEncode512(secretKey)
            };

            if (!cache.TryGetValue("user_id", out string userId))
            {
                return Unauthorized();
            }
            else
            {
                dto.UserID = userId;
            }

            Response<object> result = new Response<object>();

            try
            {
                result.Success = _facade.ChangeSecretKey(dto);

                if (!result.Success)
                {
                    result.AddError("Could not perform the operation");
                }

                
            }
            catch (Exception ex)
            {
                result.AddError(ex);
            }

            watch.Stop();

            result.ResponseTime = watch.Elapsed.TotalSeconds.ToString("0.0### seconds");
            result.HttpStatusCode = System.Net.HttpStatusCode.OK;

            return result;
        }

        private string SetCache(IMemoryCache cache, string key, string value, DateTime expirateAt)
        {
            return cache.GetOrCreate(key, context =>
            {
                context.SetAbsoluteExpiration(expirateAt);
                context.SetPriority(CacheItemPriority.High);

                return value;
            });
        }

        private string GetEncode512(string text)
        {
            SHA512 sha512 = SHA512.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            byte[] hash = sha512.ComputeHash(bytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }
    }
}

