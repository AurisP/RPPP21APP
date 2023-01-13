using System;
using System.Collections.Generic;

namespace RPPP21APP.Models;

public partial class Plant
{
    public int PlantId { get; set; }

    public string? Name { get; set; }

    public int? GroupOfPlantsId { get; set; }

    public int? PassportId { get; set; }

    public virtual Passport? Passport { get; set; }

    public virtual GroupOfPlant? GroupOfPlants { get; set; }

    //public virtual ICollection<Passport> Passports { get; } = new List<Passport>();
}
