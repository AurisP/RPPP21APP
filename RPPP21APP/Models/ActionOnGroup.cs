using System;
using System.Collections.Generic;

namespace RPPP21APP.Models;

public partial class ActionOnGroup
{
    public int ActionOnGroupId { get; set; }

    public DateTime Time { get; set; }

    public int QuantityIfHarvest { get; set; }

    public int ActionId { get; set; }

    public int GroupOfPlantsId { get; set; }

    public int? StorageId { get; set; }

    public int? MaterialUseId { get; set; }

    public int WorkerId { get; set; }

    public virtual ActionM Action { get; set; } = null!;

    public virtual GroupOfPlant GroupOfPlants { get; set; } = null!;

    public virtual MaterialUse? MaterialUse { get; set; } = null!;

    public virtual Storage? Storage { get; set; }

    public virtual Worker Worker { get; set; } = null!;
}
