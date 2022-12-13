using Microsoft.AspNetCore.Mvc;
using RPPP21APP.Models;

namespace RPPP21APP.ViewModels
{
    public class CreateWorkerViewModel
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public double Salary { get; set; }
        public string? PhoneNumber { get; set; }

        public string? Experience { get; set; }

        public int? WorkingHours { get; set; }
    }
}
