using RPPP21APP.Models;

namespace RPPP21APP.Interfaces
{
    public interface IPassportRepository
    {
        Task<IEnumerable<Passport>> GetAll();
        Task<Passport> GetByIdAsync(int id);
        Task<Passport> GetByIdAsyncNoTrack(int id);
        bool Add(Passport passport);
        bool Update(Passport passport);
        bool Delete(Passport passport);
        bool Save();
    }
}
