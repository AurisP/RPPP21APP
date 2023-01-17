using RPPP21APP.Models;

namespace RPPP21APP.Interfaces
{
    public interface IStorageRepository
    {
        Task<IEnumerable<Storage>> GetAll();
        Task<Storage> GetByIdAsync(int? id);
        Task<Storage> GetByIdAsyncNoTrack(int? id);
        bool Add(Storage storage);
        bool Update(Storage storage);
        bool Delete(Storage storage);
        bool Save();
    }
}
