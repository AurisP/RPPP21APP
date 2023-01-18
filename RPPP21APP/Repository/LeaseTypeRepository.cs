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
    public class LeaseTypeRepository : ILeaseTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public LeaseTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LeaseType>> GetAll()
        {
            return await _context.LeaseTypes.ToListAsync();
        }

        public async Task<LeaseType> GetByIdAsync(int id)
        {
            return await _context.LeaseTypes.FirstOrDefaultAsync(i => i.LeaseTypeId == id);
        }

        public async Task<LeaseType> GetByIdAsyncNoTrack(int id)
        {
            return await _context.LeaseTypes.AsNoTracking().FirstOrDefaultAsync(i => i.LeaseTypeId == id);
        }
        public bool Add(LeaseType leasetype)
        {
            _context.Add(leasetype);
            return Save();
        }

        public bool Delete(LeaseType leasetype)
        {
            _context.Remove(leasetype);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(LeaseType leasetype)
        {
            _context.Update(leasetype);
            return Save();
        }

    }
}
