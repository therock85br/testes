using System;
using System.Collections.Generic;
using System.Text;
using Attiva.Freight.Tracking.Domain.Entities;
using Attiva.Freight.Tracking.Domain.Interfaces.Service;
using Attiva.Freight.Tracking.Domain.Interfaces.Repository;

namespace Attiva.Freight.Tracking.Domain.Services
{
    public class VehicleService : IVehicleService, IDisposable
    {
        private readonly IVehicleRepository _repository;

        public VehicleService(IVehicleRepository repository)
        {
            _repository = repository;
        }

        public decimal GetLastOdometer(Vehicle item)
        {
            if (item == null || string.IsNullOrWhiteSpace(item.Tag))
            {
                throw new NullReferenceException();
            }

            return _repository.GetLastOdometer(item);
        }

        public decimal GetAverageConsumption(Vehicle item)
        {
            if (item == null || string.IsNullOrWhiteSpace(item.Tag))
            {
                throw new NullReferenceException();
            }

            return _repository.GetAverageConsumption(item);
        }

        public void Dispose()
        {
        }
    }
}
