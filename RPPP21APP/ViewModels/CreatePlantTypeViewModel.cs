using Microsoft.AspNetCore.Mvc;
using RPPP21APP.Models;
using System.ComponentModel.DataAnnotations;

namespace RPPP21APP.ViewModels
{
    public class CreatePlantTypeViewModel
    {
        public int PlantTypeId { get; set; }
        public string Type { get; set; }
        public double? CaloriesPer100g { get; set; }
        public double? FatPer100g { get; set; }
        public double? ProteinPer100g { get; set; }
        public double? FiberPer100g { get; set; }
        public double? CarbsPer100g { get; set; }
        public string? Vitamins { get; set; }

        public int PlantBiologyId { get; set; }
        public virtual ICollection<GroupOfPlant>? GroupOfPlants { get; set; }
    }
}
