using System;
using System.Collections.Generic;

namespace RPPP21APP.Models;

public partial class WeatherCondition
{
    public int WeatherConditionsId { get; set; }

    public double? SunLevel { get; set; }

    public double? WaterLevel { get; set; }

    public virtual ICollection<Plot> Plots { get; } = new List<Plot>();
}
