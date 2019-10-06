using System;
using System.Collections.Generic;
using System.Text;
using Attiva.Freight.Authorization.Domain.Entities;

namespace Attiva.Freight.Authorization.Domain.Interfaces.Repository
{
    public interface IAuthorizationRepository
    {
        UserApp Login(UserApp item);
    }
}
