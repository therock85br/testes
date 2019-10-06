using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Attiva.Freight.Authorization.Library.DTO.Access;
using Attiva.Freight.Authorization.Domain.Entities;

namespace Attiva.Freight.Authorization.Application.AutoMapper
{
    public class AuthorizationProfile : Profile
    {
        public AuthorizationProfile()
        {
            CreateMap<CredentialsDTO, UserApp>();
        }
    }
}
