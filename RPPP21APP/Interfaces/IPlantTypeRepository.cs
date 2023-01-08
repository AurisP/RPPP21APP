using RPPP21APP.Models;

namespace RPPP21APP.Interfaces
{
    public interface IPlantTypeRepository
    {
        Task<IEnumerable<PlantType>> GetAll();
        Task<PlantType> GetByIdAsync(int id);
        Task<PlantType> GetByIdAsyncNoTrack(int id);
        bool Add(PlantType planttype);
        bool Update(PlantType planttype);
        bool Delete(PlantType planttype);
        bool Save();
    }
}