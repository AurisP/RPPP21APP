using RPPP21APP.Models;

namespace RPPP21APP.Interfaces
{
    public interface IMaterialUseRepository
    {
        int Add(MaterialUse materialUse);
        bool Update(MaterialUse materialUse);
        bool Delete(MaterialUse materialUse);
        bool Save();
    }
}
