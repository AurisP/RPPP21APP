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
    public class ContractorRepository : IContractorRepository
    {
        private readonly ApplicationDbContext _context;

        public ContractorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Contractor>> GetAll()
        {
            return await _context.Contractors.ToListAsync();
        }

        public async Task<Contractor> GetByIdAsync(int id)
        {
            return await _context.Contractors.FirstOrDefaultAsync(i => i.ContractorId == id);
        }

        public async Task<Contractor> GetByIdAsyncNoTrack(int id)
        {
            return await _context.Contractors.AsNoTracking().FirstOrDefaultAsync(i => i.ContractorId == id);
        }
        public bool Add(Contractor contractor)
        {
            _context.Add(contractor);
            return Save();
        }

        public bool Delete(Contractor contractor)
        {
            _context.Remove(contractor);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Contractor contractor)
        {
            _context.Update(contractor);
            return Save();
        }

    }
}
