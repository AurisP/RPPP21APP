using Microsoft.AspNetCore.Mvc;
using RPPP21APP.Models;
using System.ComponentModel.DataAnnotations;

namespace RPPP21APP.ViewModels
{
    public class CreatePlantViewModel
    {
        public int PlantId { get; set; }
        [MinLength(3), MaxLength(20)] 
        public string? Name { get; set; }
        public int? GroupOfPlantsId { get; set; }

        public IEnumerable<Passport> Passports { get; set; }
        public int PlantTypeId { get; set; }

        public int PlotId { get; set; }
    }
}
