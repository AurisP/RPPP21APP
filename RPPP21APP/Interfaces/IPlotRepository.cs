using RPPP21APP.Models;

namespace RPPP21APP.Interfaces
{
    public interface IPlotRepository
    {
        Task<IEnumerable<Plot>> GetAll();
        Task<Plot> GetByIdAsync(int id);
        Task<Plot> GetByIdAsyncNoTrack(int id);
        bool Add(Plot plot);
        bool Update(Plot plot);
        bool Delete(Plot plot);
        bool Save();
    }
}
