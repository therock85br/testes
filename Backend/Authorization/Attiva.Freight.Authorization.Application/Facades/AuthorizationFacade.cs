using System;
using System.Collections.Generic;
using System.Text;
using Attiva.Freight.Authorization.Library.DTO.Access;
using Attiva.Freight.Authorization.Application.Interfaces;
using Attiva.Freight.Authorization.Domain.Interfaces.Service;
using Attiva.Freight.Authorization.Domain.Entities;
using AutoMapper;

namespace Attiva.Freight.Authorization.Application.Facades
{
    public class AuthorizationFacade : IAuthorizationFacade
    {
        private readonly IAuthorizationService _service;
        private readonly IMapper _mapper;

        public AuthorizationFacade(IAuthorizationService service,
                                   IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public bool Login(CredentialsDTO item)
        {
            return _service.Login(_mapper.Map<UserApp>(item)) != null;
        }

        public bool ChangeSecretKey(CredentialsDTO item)
        {
            throw new NotImplementedException();
        }
    }
}
