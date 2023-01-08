using RPPP21APP.Models;

namespace RPPP21APP.Interfaces
{
    public interface IPlantRepository
    {
        Task<IEnumerable<Plant>> GetAll();
        Task<Plant> GetByIdAsync(int id);
        Task<Plant> GetByIdAsyncNoTrack(int id);
        bool Add(Plant plant);
        bool Update(Plant plant);
        bool Delete(Plant plant);
        bool Save();
    }
}