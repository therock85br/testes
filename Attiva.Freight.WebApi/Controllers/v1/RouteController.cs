using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Attiva.Freight.Tracking.Library.DTO.Route;
using Attiva.Freight.Tracking.Application.Interfaces;
using Attiva.Communication;
using Microsoft.Extensions.Caching.Memory;
using System.Net;


namespace NetPartners.IPVData.WebApi.Controllers.v1
{
    [Route("api/v1/route")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly IRouteFacade _facade;

        public RouteController(IRouteFacade facade)
        {
            _facade = facade;
        }

        /// <summary>
        /// Retorna a data\hora prevista de chegada do frete
        /// </summary>
        /// <remarks>
        /// Obrigatório informar todos os campos.
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">Requisição atendida</response>
        /// <response code="400">Quando não informado todos os campos</response>
        /// <response code="401">Token não informado ou expirado</response>
        [Authorize("Bearer")]
        [Produces("application/json")]
        [HttpPost]
        public ActionResult<Response<RouteDTO>> Post([FromBody] RouteDTO dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            var watch = Stopwatch.StartNew();

            Response<RouteDTO> result = new Response<RouteDTO>();

            try
            {
                if (ModelState.IsValid)
                {
                    result.Items.Add(_facade.SetArriveDate(dto));
                    result.Success = result.Items.Any();
                    result.HttpStatusCode = HttpStatusCode.OK;
                }
                else
                {
                    foreach (var modelState in ModelState.Values)
                    {
                        foreach (var error in modelState.Errors)
                        {
                            result.AddError(error.ErrorMessage);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.AddError(ex);
            }

            watch.Stop();

            result.ResponseTime = watch.Elapsed.TotalSeconds.ToString("0.0### seconds");

            return result;
        }
    }
}
