using Microsoft.EntityFrameworkCore;
using RPPP21APP.Data;
using RPPP21APP.Interfaces;
using RPPP21APP.Models;

namespace RPPP21APP.Repository
{
    public class PlotRepository : IPlotRepository
    {
        private readonly ApplicationDbContext _context;

        public PlotRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Plot plot)
        {
            _context.Add(plot);
            return Save();
        }

        public bool Delete(Plot plot)
        {
            _context.Remove(plot);
            return Save();
        }

        public async Task<IEnumerable<Plot>> GetAll()
        {
            return await _context.Plots.Include(a => a.WeatherConditions).AsNoTracking().ToListAsync();
        }

        public async Task<Plot>? GetByIdAsyncNoTrack(int id)
        {
            return await _context.Plots.Include(i => i.WeatherConditions).AsNoTracking().FirstOrDefaultAsync(i => i.PlotId == id);
        }

        public async Task<Plot>? GetByIdAsync(int id)
        {
            return await _context.Plots.Include(i => i.WeatherConditions).FirstOrDefaultAsync(i => i.PlotId == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Plot plot)
        {
            _context.Update(plot);
            return Save();
        }
    }
}
