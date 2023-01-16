using Microsoft.EntityFrameworkCore;
using RPPP21APP.Data;
using RPPP21APP.Interfaces;
using RPPP21APP.Models;

namespace RPPP21APP.Repository
{
    public class PlantBiologyRepository : IPlantBiologyRepository
    {
        private readonly ApplicationDbContext _context;

        public PlantBiologyRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(PlantBiology plantBiology)
        {
            _context.Add(plantBiology);
            return Save();
        }

        public bool Delete(PlantBiology plantBiology)
        {
            _context.Remove(plantBiology);
            return Save();
        }

        public async Task<IEnumerable<PlantBiology>> GetAll()
        {
            return await _context.PlantBiologies.ToListAsync();
        }

        public async Task<PlantBiology?> GetByIdAsync(int id)
        {
            return await _context.PlantBiologies.FirstOrDefaultAsync(i => i.PlantBiologyId == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(PlantBiology plantBiology)
        {
            throw new NotImplementedException();
        }
    }
}
