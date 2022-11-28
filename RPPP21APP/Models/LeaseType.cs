using System;
using System.Collections.Generic;

namespace RPPP21APP.Models;

public partial class LeaseType
{
    public int LeaseTypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Lease> Leases { get; } = new List<Lease>();
}
