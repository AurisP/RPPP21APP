using RPPP21APP.Models;

namespace RPPP21APP.Interfaces
{
    public interface IHistoricalInfrastructureRepository
    {
        Task<IEnumerable<HistoricalInfrastructure>> GetAll();
        Task<HistoricalInfrastructure> GetByIdAsync(int id);
        Task<HistoricalInfrastructure> GetByIdAsyncNoTrack(int id);
        bool Add(HistoricalInfrastructure historicalInfrastructure);
        bool Update(HistoricalInfrastructure historicalInfrastructure);
        bool Delete(HistoricalInfrastructure historicalInfrastructure);
        bool Save();
        bool Exists(Func<object, bool> value);
    }
}