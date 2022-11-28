using System;
using System.Collections.Generic;

namespace RPPP21APP.Models;

public partial class PlotDelivery
{
    public int PlotDeliveryId { get; set; }

    public int DeliveryId { get; set; }

    public int PlotId { get; set; }

    public virtual Delivery Delivery { get; set; } = null!;

    public virtual Plot Plot { get; set; } = null!;
}
