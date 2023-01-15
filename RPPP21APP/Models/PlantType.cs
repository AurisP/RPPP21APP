using System;
using System.Collections.Generic;

namespace RPPP21APP.Models;

public partial class PlantType
{
    public int PlantTypeId { get; set; }

    public string Type { get; set; } = null!;

    public double? CaloriesPer100g { get; set; }

    public double? FatPer100g { get; set; }

    public double? ProteinPer100g { get; set; }

    public double? FiberPer100g { get; set; }

    public double? CarbsPer100g { get; set; }

    public string? Vitamins { get; set; }

    public int? PlantBiologyId { get; set; }

    public virtual ICollection<GroupOfPlant> GroupOfPlants { get; } = new List<GroupOfPlant>();

    public virtual ICollection<Storage> Storages { get; } = new List<Storage>();

    public PlantBiology? PlantBiology { get; set; }
}
