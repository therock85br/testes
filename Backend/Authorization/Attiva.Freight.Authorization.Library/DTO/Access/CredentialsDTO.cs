using System;
using System.Collections.Generic;
using System.Text;

namespace Attiva.Freight.Authorization.Library.DTO.Access
{
    public class CredentialsDTO
    {
        public string UserID { get; set; }
        public string SecretKey { get; set; }
        public string RefreshToken { get; set; }
    }
}
