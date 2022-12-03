using RPPP21APP.Models;

namespace RPPP21APP.Interfaces
{
    public interface IWeatherConditionsRepository
    {
        Task<IEnumerable<WeatherCondition>> GetAll();
        Task<WeatherCondition> GetByIdAsync(int id);
        bool Add(WeatherCondition weatherCondition);
        bool Update(WeatherCondition weatherCondition);
        bool Delete(WeatherCondition weatherCondition);
        bool Save();
    }
}
