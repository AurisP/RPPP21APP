using System;
using System.Collections.Generic;

namespace RPPP21APP.Models;

public partial class GroupOfPlant
{
    public int GroupOfPlantsId { get; set; }

    public int PlantTypeId { get; set; }

    public int GroupsOnPlotId { get; set; }

    public virtual ICollection<ActionOnGroup> ActionOnGroups { get; } = new List<ActionOnGroup>();

    public virtual ICollection<CostPerGroup> CostPerGroups { get; } = new List<CostPerGroup>();

    public virtual ICollection<Grant> Grants { get; } = new List<Grant>();

    //public virtual ICollection<PlantsReservation> GroupOfPlantsReservations { get; } = new List<PlantsReservation>();

    public virtual GroupsOnPlot GroupsOnPlot { get; set; } = null!;

    public virtual PlantType PlantType { get; set; } = null!;

    public virtual ICollection<Plant> Plants { get; } = new List<Plant>();
}
