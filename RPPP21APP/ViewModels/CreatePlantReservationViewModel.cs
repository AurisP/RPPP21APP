﻿using System.ComponentModel.DataAnnotations;

namespace RPPP21APP.ViewModels
{
    public class CreatePlantReservationViewModel
    {
        public int PlantReservationId { get; set; }
        public int Amount { get; set; }


        public int Price { get; set; }
        public int ReservationId { get; set; }

        public int PlantId { get; set; }

    }
}
