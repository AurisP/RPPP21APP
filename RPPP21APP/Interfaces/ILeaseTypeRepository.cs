using RPPP21APP.Models;

namespace RPPP21APP.Interfaces
{
    public interface ILeaseTypeRepository
    {
        Task<IEnumerable<LeaseType>> GetAll();
        Task<LeaseType> GetByIdAsync(int id);
        Task<LeaseType> GetByIdAsyncNoTrack(int id);
        bool Add(LeaseType leasetype);
        bool Update(LeaseType leasetype);
        bool Delete(LeaseType leasetype);
        bool Save();
    }
}
