using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RPPP21APP.Data;
using RPPP21APP.Interfaces;
using RPPP21APP.Models;

namespace RPPP21APP.Repositories
{
    public class InfrastructureRepository : IInfrastructureRepository
    {
        private readonly ApplicationDbContext _context;

        public InfrastructureRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Infrastructure>> GetAll()
        {
            return await _context.Infrastructures
                .Include(i => i.Plot)
                .ToListAsync();
        }

        public async Task<Infrastructure> GetByIdAsync(int id)
        {
            return await _context.Infrastructures.FindAsync(id);
        }

        public async Task<Infrastructure> GetByIdAsyncNoTrack(int id)
        {
            return await _context.Infrastructures.AsNoTracking().FirstOrDefaultAsync(x => x.InfrastructureId == id);
        }

        public bool Add(Infrastructure infrastructure)
        {
            _context.Infrastructures.Add(infrastructure);
            return Save();
        }

        public bool Update(Infrastructure infrastructure)
        {
            _context.Infrastructures.Update(infrastructure);
            return Save();
        }

        public bool Delete(Infrastructure infrastructure)
        {
            _context.Infrastructures.Remove(infrastructure);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
