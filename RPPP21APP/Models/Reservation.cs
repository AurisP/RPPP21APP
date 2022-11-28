using System;
using System.Collections.Generic;

namespace RPPP21APP.Models;

public partial class Reservation
{
    public int ReservationId { get; set; }

    public int Amount { get; set; }

    public int AgreedPrice { get; set; }

    public int CustomerId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<GroupOfPlantsReservation> GroupOfPlantsReservations { get; } = new List<GroupOfPlantsReservation>();
}
