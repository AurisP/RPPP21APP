using System;
using System.Collections.Generic;

namespace RPPP21APP.Models;

public partial class Plot
{
    public string? Name { get; set; }

    public string? Coordinates { get; set; }

    public double? Area { get; set; }

    public int PlotId { get; set; }

    public int WeatherConditionsId { get; set; }

    public virtual ICollection<GroupsOnPlot> GroupsOnPlots { get; } = new List<GroupsOnPlot>();

    public virtual ICollection<Infrastructure> Infrastructures { get; } = new List<Infrastructure>();

    public virtual ICollection<Lease> Leases { get; } = new List<Lease>();

    public virtual ICollection<PlotDelivery> PlotDeliveries { get; } = new List<PlotDelivery>();

    public virtual ICollection<Storage> Storages { get; } = new List<Storage>();

    public virtual WeatherCondition WeatherConditions { get; set; } = null!;
}
