using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace RPPP21APP.Models;

public partial class Worker
{
    public int WorkerId { get; set; }

    [Column(TypeName = "varchar(40)")]
    public string Name { get; set; } = null!;
    [Column(TypeName = "varchar(40)")]
    public string Surname { get; set; } = null!;

    public double Salary { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string Experience { get; set; } = null!;

    public double? WorkingHours { get; set; }

    public virtual ICollection<ActionOnGroup> ActionOnGroups { get; } = new List<ActionOnGroup>();
}
