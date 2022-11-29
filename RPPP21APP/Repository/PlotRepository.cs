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
            return await _context.Plots.ToListAsync();
        }

        public async Task<Plot> GetByIdasync(int id)
        {
            return await _context.Plots.FirstOrDefaultAsync(i => i.PlotId == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Plot plot)
        {
            throw new NotImplementedException();
        }
    }
}
