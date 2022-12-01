using Microsoft.EntityFrameworkCore;
using RPPP21APP.Data;
using RPPP21APP.Interfaces;
using RPPP21APP.Models;

namespace RPPP21APP.Repository
{
    public class WeatherConditionsRepository : IWeatherConditionsRepository
    {
        private readonly ApplicationDbContext _context;

        public WeatherConditionsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(WeatherCondition weatherCondition)
        {
            _context.Add(weatherCondition);
            return Save();
        }

        public bool Delete(WeatherCondition weatherCondition)
        {
            _context.Remove(weatherCondition);
            return Save();
        }

        public async Task<IEnumerable<WeatherCondition>> GetAll()
        {
            return await _context.WeatherConditions.ToListAsync();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(WeatherCondition weatherCondition)
        {
            throw new NotImplementedException();
        }
    }
}
