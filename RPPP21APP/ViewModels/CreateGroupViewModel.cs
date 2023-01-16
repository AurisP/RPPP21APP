using RPPP21APP.Models;

namespace RPPP21APP.ViewModels
{
    public class CreateGroupViewModel
    {      
        public int PlotId { get; set; }
        public IEnumerable<Plot>? Plots { get; set; }
        public virtual GroupsOnPlot GroupsOnPlot { get; set; } = new GroupsOnPlot();
        public virtual PlantType PlantType { get; set; } = null!;
        public int PlantTypeId { get; set; }
        public IEnumerable<PlantType>?  PlantTypes { get; set; }

    }
}
