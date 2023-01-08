using Microsoft.AspNetCore.Mvc;
using RPPP21APP.Models;

namespace RPPP21APP.ViewModels
{
    public class CreatePlantViewModel
    {
        public int PlantId { get; set; }
        public string? Name { get; set; }
        public int? GroupOfPlantsId { get; set; }

        public IEnumerable<Passport> Passports { get; set; }
    }
}
