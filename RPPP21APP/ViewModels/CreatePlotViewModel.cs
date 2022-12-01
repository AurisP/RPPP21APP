using RPPP21APP.Models;

namespace RPPP21APP.ViewModels
{
    public class CreatePlotViewModel
    {
        public string? Name { get; set; }
        public string? Coordinates { get; set; }
        public double? Area { get; set; }
        public int WeatherConditionsId { get; set; }
        public IEnumerable<WeatherCondition> WeatherConditions { get; set; }
    }
}
