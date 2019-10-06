using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Attiva.Freight.Tracking.Domain.Entities;
using Attiva.Freight.Tracking.Domain.Interfaces.Repository;
using Attiva.Freight.Tracking.Infrastructure.Persistence;


namespace Attiva.Freight.Tracking.Infrastructure.Repositories
{
    public class VehicleRepository : IVehicleRepository, IDisposable
    {
        public decimal GetLastOdometer(Vehicle item)
        {
            throw new NotImplementedException();
        }

        public decimal GetAverageConsumption(Vehicle item)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }
    }
}
