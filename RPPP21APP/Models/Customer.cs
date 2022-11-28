using System;
using System.Collections.Generic;

namespace RPPP21APP.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string Name { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string? Email { get; set; }

    public virtual ICollection<Reservation> Reservations { get; } = new List<Reservation>();
}
