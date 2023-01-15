using RPPP21APP.Models;

namespace RPPP21APP.Interfaces
{
    public interface IMaterialRepository
    {
        Task<IEnumerable<Material>> GetAll();
        Task<Material> GetByIdAsync(int id);
        Task<Material> GetByIdAsyncNoTrack(int id);
        Task<IEnumerable<Material>> GetListByIdAsync(int id);
        bool Add(Material material);
        bool Update(Material material);
        bool Delete(Material material);
        bool Save();
    }
}
