using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Attiva.Freight.Tracking.Library.DTO.Route;
using Attiva.Freight.Tracking.Domain.Entities;

namespace Attiva.Freight.Tracking.Application.AutoMapper
{
    public class RouteProfile : Profile
    {
        public RouteProfile()
        {
            CreateMap<RouteDTO, Route>()
                .ForPath(dst => dst.Vehicle.Tag, opt => opt.MapFrom(src => src.VehicleTag))
                .ForPath(dst => dst.Vehicle.Model, opt => opt.MapFrom(src => src.VehicleModel))
                .ForPath(dst => dst.Vehicle.Carrier.Id, opt => opt.MapFrom(src => src.CarrierId))
                .ForPath(dst => dst.Vehicle.Carrier.Name, opt => opt.MapFrom(src => src.CarrierName));

            CreateMap<Route, RouteDTO>()
                .ForMember(dst => dst.VehicleTag, opt => opt.MapFrom(src => src.Vehicle.Tag))
                .ForMember(dst => dst.VehicleModel, opt => opt.MapFrom(src => src.Vehicle.Model))
                .ForMember(dst => dst.CarrierId, opt => opt.MapFrom(src => src.Vehicle.Carrier.Id))
                .ForMember(dst => dst.CarrierName, opt => opt.MapFrom(src => src.Vehicle.Carrier.Name));
        }
    }
}
