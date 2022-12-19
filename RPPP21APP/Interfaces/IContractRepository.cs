using RPPP21APP.Models;

namespace RPPP21APP.Interfaces
{
    public interface IContractRepository
    {
        Task<IEnumerable<Contract>> GetAll();
        Task<Contract> GetByIdAsync(int id);
        Task<Contract> GetByIdAsyncNoTrack(int id);
        bool Add(Contract contract);
        bool Update(Contract contract);
        bool Delete(Contract contract);
        bool Save();
    }
}
