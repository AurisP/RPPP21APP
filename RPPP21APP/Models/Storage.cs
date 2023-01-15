using System;
using System.Collections.Generic;

namespace RPPP21APP.Models;

public partial class Storage
{
    public int StorageId { get; set; }

    public string Place { get; set; } = null!;
    
    public int PlantTypeId { get; set; }

    public int Amount { get; set; }

    public DateTime TimeOfHarvest { get; set; }

    public int PlotId { get; set; }

    public virtual ICollection<ActionOnGroup> ActionOnGroups { get; } = new List<ActionOnGroup>();

    public virtual Plot Plot { get; set; } = null!;

    public virtual PlantType PlantType { get; set; } = null!;
}
