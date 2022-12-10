using RPPP21APP.Models;

namespace RPPP21APP.Interfaces
{
    public interface IGroupOfPlants
    {
        Task<IEnumerable<PlantType>> GetAvailableTypes(int plotId);
        Task<GroupOfPlant?> GetByIdAsync(int plotId, int typeId);
        Task<GroupOfPlant?> GetByIdAsyncNoTrack(int plotId, int typeId);
        bool Add(GroupOfPlant groupOfPlant);
        bool Update(GroupOfPlant groupOfPlant);
        bool Delete(GroupOfPlant groupOfPlant);
        bool Save();
    }
}
