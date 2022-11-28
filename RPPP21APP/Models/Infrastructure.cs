using System;
using System.Collections.Generic;

namespace RPPP21APP.Models;

public partial class Infrastructure
{
    public int InfrastructureId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime BuildDate { get; set; }

    public int? Cost { get; set; }

    public int PlotId { get; set; }

    public virtual ICollection<HistoricalInfrastructure> HistoricalInfrastructures { get; } = new List<HistoricalInfrastructure>();

    public virtual Plot Plot { get; set; } = null!;
}
