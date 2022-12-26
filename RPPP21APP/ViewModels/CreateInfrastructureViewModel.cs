using Microsoft.AspNetCore.Mvc;
using RPPP21APP.Models;

namespace RPPP21APP.ViewModels
{
    public class CreateInfrastructureViewModel
    {
        public string? Name { get; set; }
        public DateTime? BuildDate { get; set; }
        public double? Cost { get; set; }
        public int PlotId { get; set; }

        public IEnumerable<HistoricalInfrastructure>? HistoricalInfrastructures { get; set; }
    }
}
