using RPPP21APP.Models;

namespace RPPP21APP.Interfaces
{
    public interface ILeaseRepository
    {
        Task<IEnumerable<Lease>> GetAll();
        Task<Lease> GetByIdAsync(int id);
        Task<Lease> GetByIdAsyncNoTrack(int id);
        bool Add(Lease lease);
        bool Update(Lease lease);
        bool Delete(Lease lease);
        bool Save();
    }
}
