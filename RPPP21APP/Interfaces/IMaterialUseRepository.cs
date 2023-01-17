using RPPP21APP.Models;

namespace RPPP21APP.Interfaces
{
    public interface IMaterialUseRepository
    {
        Task<MaterialUse> GetByIdAsync(int id);
        int Add(MaterialUse materialUse);
        bool Update(MaterialUse materialUse);
        bool Delete(MaterialUse materialUse);
        bool Save();
    }
}
