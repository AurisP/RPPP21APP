using RPPP21APP.Models;

namespace RPPP21APP.Interfaces
{
    public interface IInfrastructureRepository
    {
        Task<IEnumerable<Infrastructure>> GetAll();
        Task<Infrastructure> GetByIdAsync(int id);
        Task<Infrastructure> GetByIdAsyncNoTrack(int id);
        bool Add(Infrastructure infrastructure);
        bool Update(Infrastructure infrastructure);
        bool Delete(Infrastructure infrastructure);
        bool Save();
    }
}