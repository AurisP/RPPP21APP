using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RPPP21APP.Data;
using RPPP21APP.Interfaces;
using RPPP21APP.Models;

namespace RPPP21APP.Repositories
{
    public class PlantRepository : IPlantRepository
    {
        private readonly ApplicationDbContext _context;

        public PlantRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Plant>> GetAll()
        {
            return await _context.Plants
            .Include(i => i.GroupOfPlants)
            .ThenInclude(g => g.PlantType)
            .ToListAsync();
        }

        public async Task<Plant> GetByIdAsync(int id)
        {
            return await _context.Plants
                .Include(i => i.GroupOfPlants)
                .FirstOrDefaultAsync(i => i.PlantId == id);
        }

        public async Task<Plant> GetByIdAsyncNoTrack(int id)
        {
            return await _context.Plants.AsNoTracking().FirstOrDefaultAsync(x => x.PlantId == id);
        }

        public bool Add(Plant plant)
        {
            _context.Plants.Add(plant);
            return Save();
        }

        public bool Update(Plant plant)
        {
            _context.Plants.Update(plant);
            return Save();
        }

        public bool Delete(Plant plant)
        {
            _context.Plants.Remove(plant);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
