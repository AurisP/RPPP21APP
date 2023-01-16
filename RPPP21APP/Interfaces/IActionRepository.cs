using RPPP21APP.Models;

namespace RPPP21APP.Interfaces
{
    public interface IActionRepository
    {
        bool Add(ActionM action);
        bool Update(ActionM action);
        bool Delete(ActionM action);
        bool Save();
    }
}
