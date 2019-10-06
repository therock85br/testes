using System;
using System.Collections.Generic;
using System.Text;
using Attiva.Freight.Authorization.Domain.Entities;

namespace Attiva.Freight.Authorization.Domain.Interfaces.Service
{
    public interface IAuthorizationService
    {
        UserApp Login(UserApp item);
    }
}
