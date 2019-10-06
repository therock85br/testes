using System;
using System.Collections.Generic;
using System.Text;
using Attiva.Freight.Tracking.Domain.Entities;

namespace Attiva.Freight.Tracking.Domain.Interfaces.Service
{
    public interface IRouteService
    {
        Route SetArriveDate(Route item);
    }
}
