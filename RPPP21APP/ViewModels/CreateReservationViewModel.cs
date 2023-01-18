using RPPP21APP.Models;
using System.ComponentModel.DataAnnotations;

namespace RPPP21APP.ViewModels
{
    public class CreateReservationViewModel
    {
        public int ReservationId { get; set; }

        public IEnumerable<PlantsReservation> PlantsReservations { get; set; }
        public int CustomerId { get; set; }

    }
}
