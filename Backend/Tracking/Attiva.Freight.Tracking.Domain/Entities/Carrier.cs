using System;
using System.Collections.Generic;
using System.Text;

namespace Attiva.Freight.Tracking.Domain.Entities
{
    public class Carrier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Vehicle> Vehicles { get; set; }
    }
}
