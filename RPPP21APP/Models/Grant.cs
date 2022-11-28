using System;
using System.Collections.Generic;

namespace RPPP21APP.Models;

public partial class Grant
{
    public int GrantId { get; set; }

    public string Name { get; set; } = null!;

    public int GroupOfPlantsId { get; set; }

    public virtual GroupOfPlant GroupOfPlants { get; set; } = null!;
}
