using RPPP21APP.Models;
using System.ComponentModel.DataAnnotations;

namespace RPPP21APP.ViewModels
{
    public class CreateLeaseViewModel
    {
        [Range(0, 10000000)]
        public int Cost { get; set; }

        public int ContractId { get; set; }

        public int LeaseTypeId { get; set; }

        public int PlotId { get; set; }

        /*public virtual Contract Contract { get; set; } = null!;

        public virtual LeaseType LeaseType { get; set; } = null!;

        public virtual Plot Plot { get; set; }*/
    }
}
