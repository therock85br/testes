using System;
using System.Collections.Generic;
using System.Text;
using Attiva.Freight.Authorization.Domain.Entities;
using Attiva.Freight.Authorization.Domain.Interfaces.Service;
using Attiva.Freight.Authorization.Domain.Interfaces.Repository;

namespace Attiva.Freight.Authorization.Domain.Services
{
    public class AuthorizationService : IAuthorizationService, IDisposable
    {
        private readonly IAuthorizationRepository _repository;

        public AuthorizationService(IAuthorizationRepository repository)
        {
            _repository = repository;
        }

        public UserApp Login(UserApp item)
        {
            return _repository.Login(item);
        }

        public void Dispose()
        {
        }
    }
}
