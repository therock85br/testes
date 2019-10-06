using System;
using System.Collections.Generic;
using System.Text;
using Attiva.Freight.Tracking.Library.DTO.Route;


namespace Attiva.Freight.Tracking.Application.Interfaces
{
    public interface IRouteFacade
    {
        RouteDTO SetArriveDate(RouteDTO item);
    }
}
