using System;
using System.Collections.Generic;

namespace RPPP21APP.Models;

public partial class HistoricalInfrastructure
{
    public int HistoricalInfrastructureId { get; set; }

    public DateTime DateOfdestrcution { get; set; }

    public string ReasonOfDestruction { get; set; } = null!;

    public int? CostOfDestruction { get; set; }

    public int? EarningsOnMaterials { get; set; }

    public int InfrastructureId { get; set; }

    public virtual Infrastructure Infrastructure { get; set; } = null!;
}
