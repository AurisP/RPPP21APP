using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RPPP21APP.Data;
using RPPP21APP.Interfaces;
using RPPP21APP.Models;

namespace RPPP21APP.Repositories
{
    public class HistoricalInfrastructureRepository : IHistoricalInfrastructureRepository
    {
        private readonly ApplicationDbContext _context;

        public HistoricalInfrastructureRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HistoricalInfrastructure>> GetAll()
        {
            return await _context.HistoricalInfrastructures.ToListAsync();
        }

        public async Task<HistoricalInfrastructure> GetByIdAsync(int id)
        {
            return await _context.HistoricalInfrastructures.FindAsync(id);
        }

        public async Task<HistoricalInfrastructure> GetByIdAsyncNoTrack(int id)
        {
            return await _context.HistoricalInfrastructures.AsNoTracking().FirstOrDefaultAsync(x => x.InfrastructureId == id);
        }

        public bool Add(HistoricalInfrastructure historicalInfrastructure)
        {
            _context.HistoricalInfrastructures.Add(historicalInfrastructure);
            return Save();
        }

        public bool Update(HistoricalInfrastructure historicalInfrastructure)
        {
            _context.HistoricalInfrastructures.Update(historicalInfrastructure);
            return Save();
        }

        public bool Delete(HistoricalInfrastructure historicalInfrastructure)
        {
            _context.HistoricalInfrastructures.Remove(historicalInfrastructure);
            return Save();
        }
        public bool Exists(Func<object, bool> predicate)
        {
            // Find the first infrastructure that matches the predicate
            var infrastructure = _context.Infrastructures.FirstOrDefault(predicate);

            // Return true if an infrastructure was found, false otherwise
            return infrastructure != null;
        }


        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
