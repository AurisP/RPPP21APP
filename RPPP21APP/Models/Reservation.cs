using System;
using System.Collections.Generic;

namespace RPPP21APP.Models;

public partial class Reservation
{
    public int ReservationId { get; set; }


    public int CustomerId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<PlantsReservation> PlantsReservations { get; } = new List<PlantsReservation>();
}
