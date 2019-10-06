using System;
using System.Collections.Generic;
using System.Text;
using Attiva.Freight.Authorization.Library.DTO.Access;

namespace Attiva.Freight.Authorization.Application.Interfaces
{
    public interface IAuthorizationFacade
    {
        bool Login(CredentialsDTO item);
        bool ChangeSecretKey(CredentialsDTO item);
    }
}
