using RPPP21APP.Models;

namespace RPPP21APP.Interfaces
{
    public interface IWorkerRepository
    {
        Task<IEnumerable<Worker>> GetAll();
        Task<Worker> GetByIdAsync(int id);
        Task<Worker> GetByIdAsyncNoTrack(int id);
        bool Add(Worker worker);
        bool Update(Worker worker);
        bool Delete(Worker worker);
        bool Save();
    }
}
