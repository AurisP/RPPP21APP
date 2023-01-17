using RPPP21APP.Models;

namespace RPPP21APP.Interfaces
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetAll();
        Task<Reservation> GetByIdAsync(int id);
        Task<Reservation> GetByIdAsyncNoTrack(int id);
        Task<int> GetCountAsync();
        Task<IEnumerable<Reservation>> GetSliceAsync(int offset, int size);
        bool Add(Reservation reservation);
        bool Update(Reservation reservation);
        bool Delete(Reservation reservation);
        bool Save();
    }
}
