using RPPP21APP.Models;

namespace RPPP21APP.Interfaces
{
    public interface IPlantReservationRepository
    {
        Task<IEnumerable<PlantsReservation>> GetAll();
        Task<PlantsReservation> GetByIdAsync(int id);
        Task<PlantsReservation> GetByIdAsyncNoTrack(int id);

        Task<IEnumerable<PlantsReservation>> GetByReservationIdWithPlantAsync(int ReservationId);
        Task<int> GetCountAsync();
        Task<IEnumerable<PlantsReservation>> GetSliceAsync(int offset, int size);
        bool Add(PlantsReservation plantsReservation);
        bool Update(PlantsReservation plantsReservation);
        bool Delete(PlantsReservation plantsReservation);
        bool Save();
    }
}
