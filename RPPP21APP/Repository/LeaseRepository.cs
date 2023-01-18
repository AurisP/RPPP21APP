using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RPPP21APP.Data;
using RPPP21APP.Interfaces;
using RPPP21APP.Models;

namespace RPPP21APP.Repository
{
    public class LeaseRepository : ILeaseRepository
    {
        private readonly ApplicationDbContext _context;

        public LeaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Lease>> GetAll()
        {
            return await _context.Leases
                .Include(i => i.Plot)
                .Include(i => i.Contract)
                .Include(i => i.LeaseType)
                .ToListAsync();
        }

        public async Task<Lease> GetByIdAsync(int id)
        {
            return await _context.Leases
                .Include(i => i.Plot)
                .Include(i => i.Contract)
                .Include(i => i.LeaseType)
                .FirstOrDefaultAsync(i => i.LeaseId == id);
        }

        public async Task<Lease> GetByIdAsyncNoTrack(int id)
        {
            return await _context.Leases
                .Include(i => i.Plot)
                .Include(i => i.Contract)
                .Include(i => i.LeaseType)
                .AsNoTracking().FirstOrDefaultAsync(i => i.LeaseId == id);
        }
        public bool Add(Lease lease)
        {
            _context.Leases.Add(lease);
            return Save();
        }

        public bool Delete(Lease lease)
        {
            _context.Leases.Remove(lease);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Lease lease)
        {
            _context.Leases.Update(lease);
            return Save();
        }

    }
}

