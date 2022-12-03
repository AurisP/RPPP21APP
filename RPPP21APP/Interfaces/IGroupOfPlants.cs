using RPPP21APP.Models;

namespace RPPP21APP.Interfaces
{
    public interface IGroupOfPlants
    {
        Task<IEnumerable<GroupOfPlant>> GetAll();
        Task<GroupOfPlant>? GetByIdAsync(int PlotId, int TypeId);
        Task<GroupOfPlant> GetByIdAsyncNoTrack(int PlotId, int TypeId);
        bool Add(GroupOfPlant groupOfPlant);
        bool Update(GroupOfPlant groupOfPlant);
        bool Delete(GroupOfPlant groupOfPlant);
        bool Save();
    }
}
