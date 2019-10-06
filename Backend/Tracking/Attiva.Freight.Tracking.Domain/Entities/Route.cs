using System;
using System.Collections.Generic;
using System.Text;

namespace Attiva.Freight.Tracking.Domain.Entities
{
    public class Route
    {
        public string Source { get; set; }
        public string Target { get; set; }
        public double? AverageSpeed { get; set; }
        public double? Distance { get; set; }
        public DateTime? Departure { get; set; }
        public DateTime? Arrival { get; set; }
        public Vehicle Vehicle { get; set; }
        public string cd_tag { get; set; }
    }
}
