using Microsoft.AspNetCore.Mvc;
using RPPP21APP.Models;

namespace RPPP21APP.ViewModels
{
    public class CreateHistoricalInfrastructureViewModel
    {
        public int HistoricalInfrastructureId { get; set; }
        public DateTime DateOfDestruction { get; set; }
        public string ReasonOfDestruction { get; set; }
        public int? CostOfDestruction { get; set; }
        public int? EarningsOnMaterials { get; set; }
        public int InfrastructureId { get; set; }
        public virtual Infrastructure Infrastructure { get; set; }

    }
}
