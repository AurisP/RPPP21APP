using System;
using System.Collections.Generic;

namespace RPPP21APP.Models;

public partial class MaterialUse
{
    public int MaterialUseId { get; set; }

    public int Amount { get; set; }

    public int MaterialId { get; set; }

    public virtual ICollection<ActionOnGroup> ActionOnGroups { get; } = new List<ActionOnGroup>();

    public virtual Material Material { get; set; } = null!;
}
