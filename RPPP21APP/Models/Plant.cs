using System;
using System.Collections.Generic;

namespace RPPP21APP.Models;

public partial class Plant
{
    public int PlantId { get; set; }

    public int? Name { get; set; }

    public int? GroupOfPlantsId { get; set; }

    public virtual GroupOfPlant? GroupOfPlants { get; set; }

    public virtual ICollection<Passport> Passports { get; } = new List<Passport>();
}
