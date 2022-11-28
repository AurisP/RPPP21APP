using System;
using System.Collections.Generic;

namespace RPPP21APP.Models;

public partial class ActionM
{
    public int ActionId { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<ActionOnGroup> ActionOnGroups { get; } = new List<ActionOnGroup>();
}
