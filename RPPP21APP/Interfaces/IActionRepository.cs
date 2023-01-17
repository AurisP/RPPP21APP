using RPPP21APP.Models;

namespace RPPP21APP.Interfaces
{
    public interface IActionRepository
    {
        Task<ActionM> GetByIdAsync(int id);
        bool Add(ActionM action);
        bool Update(ActionM action);
        bool Delete(ActionM action);
        bool Save();
    }
}
