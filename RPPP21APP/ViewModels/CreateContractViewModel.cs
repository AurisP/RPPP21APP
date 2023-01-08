 using RPPP21APP.Models;
using System.ComponentModel.DataAnnotations;

namespace RPPP21APP.ViewModels
{
    public class CreateContractViewModel
    {
        public DateTime Date { get; set; }
        public DateTime? ExpiryDate { get; set; }
        [MinLength(3), Required]
        public string? Description { get; set; }
        public int ContractorId { get; set; }
        public IEnumerable<Contractor>? Contractors { get; set; }
    }
}
