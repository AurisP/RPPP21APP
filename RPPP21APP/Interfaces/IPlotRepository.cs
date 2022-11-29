using RPPP21APP.Models;

namespace RPPP21APP.Interfaces
{
    public interface IPlotRepository
    {
        Task<IEnumerable<Plot>> GetAll();
        Task<Plot> GetByIdasync(int id);

        //Task<IEnumerable<Plot>> GetClubByCity(string city);
        bool Add(Plot club);
        bool Update(Plot club);
        bool Delete(Plot club);
        bool Save();
    }
}
