using System;
using System.Collections.Generic;

namespace RPPP21APP.Models;

public partial class Delivery
{
    public int DeliveryId { get; set; }

    public TimeSpan Time { get; set; }

    public double SoldPrice { get; set; }

    public double SizeOfDelivery { get; set; }

    public string Address { get; set; } = null!;

    public int DeliveryTypesId { get; set; }

    public virtual DeliveryType DeliveryTypes { get; set; } = null!;

    public virtual ICollection<PlotDelivery> PlotDeliveries { get; } = new List<PlotDelivery>();
}
