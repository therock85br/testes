using System;
using System.Collections.Generic;
using System.Text;
using Attiva.Freight.Tracking.Library.DTO.Route;
using Attiva.Freight.Tracking.Application.Interfaces;
using Attiva.Freight.Tracking.Domain.Interfaces.Service;
using Attiva.Freight.Tracking.Domain.Entities;
using AutoMapper;

namespace Attiva.Freight.Tracking.Application.Facades
{
    public class RouteFacade : IRouteFacade
    {
        private readonly IRouteService _service;
        private readonly IMapper _mapper;

        public RouteFacade(IRouteService service,
                           IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public RouteDTO SetArriveDate(RouteDTO item)
        {
            return _mapper.Map<RouteDTO>(_service.SetArriveDate(_mapper.Map<Route>(item)));
        }
    }
}
