using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RPPP21APP.Data;
using RPPP21APP.Interfaces;
using RPPP21APP.Models;

namespace RPPP21APP.Repositories
{
    public class WorkerRepository : IWorkerRepository
    {
        private readonly ApplicationDbContext _context;
        
        public WorkerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Worker>> GetAll()
        {

            return await _context.Workers.ToListAsync();
        }

        public async Task<Worker> GetByIdAsync(int id)
        {
            return await _context.Workers.FindAsync(id);
        }

        public async Task<Worker> GetByIdAsyncNoTrack(int id)
        {
            return await _context.Workers.AsNoTracking().FirstOrDefaultAsync(w => w.WorkerId == id);
        }

        public bool Add(Worker worker)
        {
            _context.Workers.Add(worker);
            return Save();
        }

        public bool Update(Worker worker)
        {
            _context.Workers.Update(worker);
            return Save();
        }

        public bool Delete(Worker worker)
        {
            _context.Workers.Remove(worker);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
