using RPPP21APP.Models;

namespace RPPP21APP.Interfaces
{
    public interface IActionOnGroupRepository
    {
        Task<IEnumerable<ActionOnGroup>> GetAll();
        Task<ActionOnGroup> GetByIdAsync(int id);
        Task<ActionOnGroup> GetByIdAsyncNoTrack(int id);
        Task<IEnumerable<ActionOnGroup>> GetListByIdAsync(int id);
        bool Add(ActionOnGroup action);
        bool Update(ActionOnGroup action);
        bool Delete(ActionOnGroup action);
        bool Save();
    }
}
