using RPPP21APP.Models;

namespace RPPP21APP.ViewModels
{
    public class CreateActionOnGroupViewModel
    {
        public int GroupOfPlantsId { get; set; }
        public DateTime Time { get; set; }
        public int QuantityIfHarvest { get; set; }
        public int WorkerId { get; set; }      
        public int ActionId { get; set; }
        public ActionM? ActionM { get; set; }
        public int MaterialUseId { get; set; }
        public MaterialUse? materialUse { get; set; }
        public int MaterialId { get; set; }
        public IEnumerable<Material>? Materials { get; set; }
        public int StorageId { get; set; } = 0;
        public virtual IEnumerable<Worker>? Workers { get; set; }
        public virtual Storage? Storage { get; set; }
        public bool harvest { get; set; }

    }
}
