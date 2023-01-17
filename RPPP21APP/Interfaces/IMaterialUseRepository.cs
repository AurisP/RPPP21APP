using RPPP21APP.Models;

namespace RPPP21APP.Interfaces
{
    public interface IMaterialUseRepository
    {
        Task<MaterialUse> GetByIdAsync(int? id);
        Task<MaterialUse> GetByUseIdAsync(int? id);
        Task<MaterialUse> GetByIdAsyncNoTrack(int? id);
        int Add(MaterialUse materialUse);
        int Update(MaterialUse materialUse);
        bool Delete(MaterialUse materialUse);
        bool Save();
    }
}
