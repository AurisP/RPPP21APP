using System.ComponentModel.DataAnnotations;

namespace RPPP21APP.ViewModels
{
    public class CreatePlantReservationViewModel
    {
        public int PlantReservationId { get; set; }
        public double Ammount { get; set; }

        public double AgreedPrice { get; set; }
        public int ReservationId { get; set; }

        public int PlantId { get; set; }

    }
}
