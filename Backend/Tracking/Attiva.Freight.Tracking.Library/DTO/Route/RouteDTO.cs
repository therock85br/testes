using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Attiva.Freight.Tracking.Library.DTO.Route
{
    public class RouteDTO
    {
        [Required]
        public string Source { get; set; }
        [Required]
        public string Target { get; set; }
        [Required]
        [Range(0.01, double.MaxValue)]
        public double? AverageSpeed { get; set; }
        [Required]
        [Range(0.01, double.MaxValue)]
        public double? Distance { get; set; }
        [Required]
        public DateTime? Departure { get; set; }
        public DateTime? Arrival { get; set; }
        [Required]
        public string VehicleTag { get; set; }
        [Required]
        public string VehicleModel { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int? CarrierId { get; set; }
        [Required]
        public string CarrierName { get; set; }
    }
}
