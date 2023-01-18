using System;
using System.Collections.Generic;

namespace RPPP21APP.Models;

public partial class Lease
{
    public int LeaseId { get; set; }

    public int Cost { get; set; }

    public int ContractId { get; set; }

    public int LeaseTypeId { get; set; }

    public int PlotId { get; set; }

    public virtual Contract Contract { get; set; } = null!;

    public virtual LeaseType LeaseType { get; set; } = null!;

    public virtual Plot Plot { get; set; }
}
