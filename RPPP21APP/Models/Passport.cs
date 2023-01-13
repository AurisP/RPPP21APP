using System;
using System.Collections.Generic;

namespace RPPP21APP.Models;

public partial class Passport
{
    public int PassportId { get; set; }

    public string LatinName { get; set; } = null!;

    public string Origin { get; set; } = null!;

    public string MotherFarm { get; set; } = null!;

    public string? LinkToFloraCroatia { get; set; }

    public int PlantId { get; set; }

    public virtual Plant Plant { get; set; } = null!;

    public virtual ICollection<Plant> Plants { get; } = new List<Plant>();
}
