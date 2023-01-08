using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPPP21APP.ViewModels
{
    public class CreateContractorViewModel
    {
        [MinLength(3), MaxLength(15)]
        [RegularExpression(@"^[A-Z]+.+",
            ErrorMessage = "Must start with uppercase")]
        public string Name { get; set; } = null!;

        [MinLength(3), MaxLength(15)]
        [RegularExpression(@"^[A-Z]+.+",
            ErrorMessage = "Must start with uppercase")]
        public string Surname { get; set; } = null!;

        [MinLength(9), MaxLength(15)]
        [Phone]
        public string PhoneNumber { get; set; } = null!;

        [EmailAddress] 
        public string Email { get; set; } = null!;

        [MinLength(5)] 
        public string Address { get; set; } = null!;
    }
}
