using System;
using Microsoft.Extensions.DependencyInjection;
using Attiva.Freight.Authorization.Application.Facades;
using Attiva.Freight.Authorization.Application.Interfaces;
using Attiva.Freight.Authorization.Domain.Services;
using Attiva.Freight.Authorization.Domain.Interfaces.Service;
using Attiva.Freight.Authorization.Domain.Interfaces.Repository;
using Attiva.Freight.Authorization.Infrastructure.Repositories;
using Attiva.Freight.Tracking.Application.Facades;
using Attiva.Freight.Tracking.Application.Interfaces;
using Attiva.Freight.Tracking.Domain.Services;
using Attiva.Freight.Tracking.Domain.Interfaces.Service;
using Attiva.Freight.Tracking.Domain.Interfaces.Repository;
using Attiva.Freight.Tracking.Infrastructure.Repositories;

namespace Attiva.Freight.ServiceLocator
{
    public static class Config
    {
        public static void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IAuthorizationFacade, AuthorizationFacade>();
            serviceCollection.AddTransient<IAuthorizationService, AuthorizationService>();
            serviceCollection.AddTransient<IAuthorizationRepository, AuthorizationRepository>();

            serviceCollection.AddTransient<IRouteFacade, RouteFacade>();
            serviceCollection.AddTransient<IRouteService, RouteService>();
        }
    }
}
