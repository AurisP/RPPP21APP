using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RPPP21APP.Data;
using RPPP21APP.Interfaces;
using RPPP21APP.Models;

namespace RPPP21APP.Repositories
{
    public class PlantTypeRepository : IPlantTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public PlantTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PlantType>> GetAll()
        {
            return await _context.PlantTypes
            .Include(i => i.GroupOfPlants)
            .ToListAsync();
        }

        public async Task<PlantType> GetByIdAsync(int id)
        {
            return await _context.PlantTypes
                .Include(i => i.GroupOfPlants)
                .FirstOrDefaultAsync(i => i.PlantTypeId == id);
        }

        public async Task<PlantType> GetByIdAsyncNoTrack(int id)
        {
            return await _context.PlantTypes.AsNoTracking().FirstOrDefaultAsync(x => x.PlantTypeId == id);
        }

        public bool Add(PlantType planttype)
        {
            _context.PlantTypes.Add(planttype);
            return Save();
        }

        public bool Update(PlantType planttype)
        {
            _context.PlantTypes.Update(planttype);
            return Save();
        }

        public bool Delete(PlantType planttype)
        {
            _context.PlantTypes.Remove(planttype);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
