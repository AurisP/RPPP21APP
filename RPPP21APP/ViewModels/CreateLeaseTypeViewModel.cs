using RPPP21APP.Models;
using System.ComponentModel.DataAnnotations;

namespace RPPP21APP.ViewModels
{
    public class CreateLeaseTypeViewModel
    {
        [MinLength(3)]
        public string Name { get; set; } = null!;
    }

}
