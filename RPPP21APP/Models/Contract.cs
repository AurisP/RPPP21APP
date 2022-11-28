using System;
using System.Collections.Generic;

namespace RPPP21APP.Models;

public partial class Contract
{
    public int ContractId { get; set; }

    public DateTime Date { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public string? Description { get; set; }

    public int ContractorId { get; set; }

    public virtual Contractor Contractor { get; set; } = null!;

    public virtual ICollection<Lease> Leases { get; } = new List<Lease>();
}
