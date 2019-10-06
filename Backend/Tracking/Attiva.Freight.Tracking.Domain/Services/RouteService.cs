using System;
using System.Collections.Generic;
using System.Text;
using Attiva.Freight.Tracking.Domain.Entities;
using Attiva.Freight.Tracking.Domain.Interfaces.Service;
using Attiva.Freight.Tracking.Domain.Interfaces.Repository;

namespace Attiva.Freight.Tracking.Domain.Services
{
    public class RouteService : IRouteService, IDisposable
    {
        public Route SetArriveDate(Route item)
        {
            if (item == null ||
                !item.AverageSpeed.HasValue ||
                !item.Distance.HasValue ||
                !item.Departure.HasValue)
            {
                throw new NullReferenceException();
            }

            item.Arrival = item.Departure.Value.AddHours(item.Distance.Value / item.AverageSpeed.Value);

            return item;
        }

        public void Dispose()
        {
        }
    }
}
