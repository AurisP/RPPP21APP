using System;
using System.Collections.Generic;

namespace RPPP21APP.Models;

public partial class CostType
{
    public int CostTypesId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<CostIncurred> CostIncurreds { get; } = new List<CostIncurred>();
}
