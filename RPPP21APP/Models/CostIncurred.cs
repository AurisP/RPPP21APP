using System;
using System.Collections.Generic;

namespace RPPP21APP.Models;

public partial class CostIncurred
{
    public int CostIncurredId { get; set; }

    public int Cost { get; set; }

    public string Description { get; set; } = null!;

    public int CostPerGroupId { get; set; }

    public int? CostTypesId { get; set; }

    public virtual CostPerGroup CostPerGroup { get; set; } = null!;

    public virtual CostType? CostTypes { get; set; }
}
