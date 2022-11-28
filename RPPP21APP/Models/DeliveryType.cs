using System;
using System.Collections.Generic;

namespace RPPP21APP.Models;

public partial class DeliveryType
{
    public int DeliveryTypesId { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<Delivery> Deliveries { get; } = new List<Delivery>();
}
