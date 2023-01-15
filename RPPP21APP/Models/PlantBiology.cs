using System;
using System.Collections.Generic;

namespace RPPP21APP.Models
{
    public partial class PlantBiology
    {
        public int PlantBiologyId { get; set; }
        public string? Name { get; set; }
        public virtual ICollection<PlantType> PlantTypes { get; } = new List<PlantType>();
    }
}
