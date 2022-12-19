using RPPP21APP.Models;

namespace RPPP21APP.Interfaces
{
    public interface IContractorRepository
    {
        Task<IEnumerable<Contractor>> GetAll();
        Task<Contractor> GetByIdAsync(int id);
        Task<Contractor> GetByIdAsyncNoTrack(int id);
        bool Add(Contractor contractor);
        bool Update(Contractor contractor);
        bool Delete(Contractor contractor);
        bool Save();
    }
}
