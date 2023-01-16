using RPPP21APP.Models;

namespace RPPP21APP.Interfaces
{
    public interface IPlantBiologyRepository
    {
        Task<IEnumerable<PlantBiology>> GetAll();
        Task<PlantBiology> GetByIdAsync(int id);
        bool Add(PlantBiology plantBiology);
        bool Update(PlantBiology plantBiology);
        bool Delete(PlantBiology plantBiology);
        bool Save();
    }
}
