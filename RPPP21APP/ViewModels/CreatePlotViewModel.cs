using RPPP21APP.Models;
using System.ComponentModel.DataAnnotations;

namespace RPPP21APP.ViewModels
{
    public class CreatePlotViewModel
    {
        [MinLength(3), MaxLength(30)] 
        public string? Name { get; set; }
        [RegularExpression(@"^[-+]?([1-8]?\d(\.\d+)?|90(\.0+)?),\s*[-+]?(180(\.0+)?|((1[0-7]\d)|([1-9]?\d))(\.\d+)?)$",
            ErrorMessage = "Incorrect coordinates")]
        public string? Coordinates { get; set; }
        [Range(1,1000000)]
        public double? Area { get; set; }
        public int WeatherConditionsId { get; set; }
        public IEnumerable<WeatherCondition>? WeatherConditions { get; set; }
    }
}
