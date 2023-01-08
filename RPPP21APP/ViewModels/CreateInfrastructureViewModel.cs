using Microsoft.AspNetCore.Mvc;
using RPPP21APP.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPPP21APP.ViewModels
{
    public class CreateInfrastructureViewModel
    {
        [MinLength(3)]
        public string? Name { get; set; }
        public DateTime? BuildDate { get; set; }
        [Range(0, 10000000)]
        public double? Cost { get; set; }
        public int PlotId { get; set; }
        public Infrastructure Infrastructure { get; set; }
        public IEnumerable<HistoricalInfrastructure>? HistoricalInfrastructures { get; set; }
    }
}
