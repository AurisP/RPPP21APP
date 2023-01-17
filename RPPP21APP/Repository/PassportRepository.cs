using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RPPP21APP.Data;
using RPPP21APP.Interfaces;
using RPPP21APP.Models;

namespace RPPP21APP.Repositories
{
    public class PassportRepository : IPassportRepository
    {
        private readonly ApplicationDbContext _context;
        
        public PassportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Passport>> GetAll()
        {

            return await _context.Passports
                .Include(i => i.Plant)
                .ToListAsync();
        }

        public async Task<Passport> GetByIdAsync(int id)
        {
            return await _context.Passports.FindAsync(id);
        }

        public async Task<Passport> GetByIdAsyncNoTrack(int id)
        {
            return await _context.Passports.AsNoTracking().FirstOrDefaultAsync(w => w.PassportId == id);
        }

        public async Task<Passport> GetDetailed(int id)
        {
            return await _context.Passports.AsNoTracking().FirstOrDefaultAsync(w => w.PassportId == id);
                
        }

        public bool Add(Passport passport)
        {
            _context.Passports.Add(passport);
            return Save();
        }

        public bool Update(Passport passport)
        {
            _context.Passports.Update(passport);
            return Save();
        }

        public bool Delete(Passport passport)
        {
            _context.Passports.Remove(passport);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
