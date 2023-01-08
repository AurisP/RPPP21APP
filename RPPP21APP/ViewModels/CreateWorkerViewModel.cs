using Microsoft.AspNetCore.Mvc;
using RPPP21APP.Models;
using System.ComponentModel.DataAnnotations;

namespace RPPP21APP.ViewModels
{
    public class CreateWorkerViewModel
    {
        [MinLength(3), MaxLength(15)]
        [RegularExpression(@"^[A-Z]+.+",
            ErrorMessage = "Must start with uppercase")]
        public string? Name { get; set; }

        [MinLength(3), MaxLength(15)]
        [RegularExpression(@"^[A-Z]+.+",
            ErrorMessage = "Must start with uppercase")]
        public string? Surname { get; set; }

        [Range(1,100000)]
        public double Salary { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        [MinLength(2)]
        public string? Experience { get; set; }

        [Range(1, 1500)] 
        public int? WorkingHours { get; set; }
    }
}
