using System;
using System.Collections.Generic;
using System.Text;
using Attiva.Freight.Tracking.Domain.Entities;

namespace Attiva.Freight.Tracking.Domain.Interfaces.Service
{
    public interface IVehicleService
    {
        decimal GetLastOdometer(Vehicle item);
        decimal GetAverageConsumption(Vehicle item);
    }
}
