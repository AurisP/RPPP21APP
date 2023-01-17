using System.ComponentModel.DataAnnotations;

namespace RPPP21APP.ViewModels
{
    public class CreateReservationViewModel
    {
        public int ReservationId { get; set; }

        public double Ammount { get; set; }

        public double AgreedPrice { get; set; }

        public int CustomerId { get; set; }

    }
}
