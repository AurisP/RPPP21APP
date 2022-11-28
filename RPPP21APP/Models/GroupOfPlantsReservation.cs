using System;
using System.Collections.Generic;

namespace RPPP21APP.Models;

public partial class GroupOfPlantsReservation
{
    public int GroupOfPlantsReservationId { get; set; }

    public int ReservationId { get; set; }

    public int GroupOfPlantsId { get; set; }

    public virtual GroupOfPlant GroupOfPlants { get; set; } = null!;

    public virtual Reservation Reservation { get; set; } = null!;
}
