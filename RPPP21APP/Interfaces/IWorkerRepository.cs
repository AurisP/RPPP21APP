using RPPP21APP.Models;

namespace RPPP21APP.Interfaces
{
    public interface IWorkerRepository
    {
        Task<IEnumerable<Worker>> GetAll();
        Task<Worker> GetByIdAsync(int id);
        Task<Worker> GetByIdAsyncNoTrack(int id);
        Task<int> GetCountAsync();
        Task<IEnumerable<Worker>> GetSliceAsync(int offset, int size);
        bool Add(Worker worker);
        bool Update(Worker worker);
        bool Delete(Worker worker);
        bool Save();
    }
}
