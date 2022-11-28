using System;
using System.Collections.Generic;

namespace RPPP21APP.Models;

public partial class Material
{
    public int MaterialId { get; set; }

    public int? AmountInStorage { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<MaterialUse> MaterialUses { get; } = new List<MaterialUse>();
}
