using Microsoft.AspNetCore.Mvc;
using RPPP21APP.Models;
using System.ComponentModel.DataAnnotations;

namespace RPPP21APP.ViewModels
{
    public class CreateHistoricalInfrastructureViewModel
    {
        public int HistoricalInfrastructureId { get; set; }
        public DateTime DateOfDestruction { get; set; }
        [MinLength(5)]
        public string ReasonOfDestruction { get; set; }
        [Range(0, 10000000)]
        public int CostOfDestruction { get; set; }
        [Range(0, 10000000)]
        public int EarningsOnMaterials { get; set; }
        public int InfrastructureId { get; set; }
        //public virtual Infrastructure Infrastructure { get; set; }

    }
}
