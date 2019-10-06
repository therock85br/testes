using System;
using System.Collections.Generic;
using System.Text;

namespace Attiva.Freight.Authorization.Library.DTO.Access
{
    public class TokenResponseDTO
    {
        public string Token { get; set; }
        public int TokenExpirate { get; set; }
        public string RefreshToken { get; set; }
        public int RefreshTokenExpirate { get; set; }
    }
}
