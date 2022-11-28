using System;
using System.Collections.Generic;

namespace RPPP21APP.Models;

public partial class CostPerGroup
{
    public int CostPerGroupId { get; set; }

    public int GroupOfPlantsId { get; set; }

    public virtual ICollection<CostIncurred> CostIncurreds { get; } = new List<CostIncurred>();

    public virtual GroupOfPlant GroupOfPlants { get; set; } = null!;
}
