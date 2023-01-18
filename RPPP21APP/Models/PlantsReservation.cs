using System;
using System.Collections.Generic;

namespace RPPP21APP.Models;

public partial class PlantsReservation
{
    public int PlantsReservationId { get; set; }

    public int ReservationId { get; set; }

    public int PlantId { get; set; }
    public int Amount { get; set; }

    public int Price { get; set; }

    public virtual Plant Plants { get; set; } = null!;

    public virtual Reservation Reservation { get; set; } = null!;
}
