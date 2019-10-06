using System;
using System.Collections.Generic;
using System.Text;

namespace Attiva.Freight.Tracking.Domain.Entities
{
    public class Vehicle
    {
        public string Tag { get; set; }
        public string Model { get; set; }
        public Carrier Carrier { get; set; }
        public int id_carrier { get; set; }
        //public List<Route> Routes { get; set; }
    }
}
