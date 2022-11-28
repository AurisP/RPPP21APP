using System;
using System.Collections.Generic;

namespace RPPP21APP.Models;

public partial class GroupsOnPlot
{
    public int GroupsOnPlotId { get; set; }

    public DateTime PlantTime { get; set; }

    public DateTime? DePlantTime { get; set; }

    public int Quantity { get; set; }

    public int PlotId { get; set; }

    public virtual ICollection<GroupOfPlant> GroupOfPlants { get; } = new List<GroupOfPlant>();

    public virtual Plot Plot { get; set; } = null!;
}
