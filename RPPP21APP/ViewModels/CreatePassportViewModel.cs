using System.ComponentModel.DataAnnotations;

namespace RPPP21APP.ViewModels
{
    public class CreatePassportViewModel
    {
        public int PassportId { get; set; }
        [MinLength(3), MaxLength(30)] 
        public string LatinName { get; set; }
        [MinLength(3), MaxLength(30)] 
        public string Origin { get; set; }
        [MinLength(3), MaxLength(30)]
        public string MotherFarm { get; set; }
        public string? LinkToFloraCroatia { get; set; }
        public int PlantId { get; set; }

    }
}
