using System;
using System.Collections.Generic;
using System.Text;

namespace Attiva.Freight.Authorization.Domain.Entities
{
    public class UserApp
    {
        public string UserId { get; set; }
        public string SecretKey { get; set; }
    }
}
