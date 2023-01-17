using Microsoft.AspNetCore.Mvc;
using RPPP21APP.Models;
using System.ComponentModel.DataAnnotations;

namespace RPPP21APP.ViewModels
{
    public class CreateCustomerViewModel
    {
        [MinLength(3), MaxLength(15)]
        [RegularExpression(@"^[A-Z]+.+",
            ErrorMessage = "Must start with uppercase")]
        public string? Name { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

    }
}
