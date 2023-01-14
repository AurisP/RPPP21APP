using RPPP21APP.Models;

namespace RPPP21APP.ViewModels
{
    public class CreateActionOnGroupViewModel
    {
        public DateTime Time { get; set; }
        public int QuantityIfHarvest { get; set; }
        public int WorkerId { get; set; }      
        public int ActionId { get; set; }
        public int MaterialUseId { get; set; }
        public int StorageId { get; set; }
        public virtual IEnumerable<Worker>? Workers { get; set; }

    }
}
