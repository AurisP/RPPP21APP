using Microsoft.EntityFrameworkCore;
using RPPP21APP.Data;
using RPPP21APP.Interfaces;
using RPPP21APP.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RPPP21APP.Repository
{
    public class MaterialRepository : IMaterialRepository
    {
        private readonly ApplicationDbContext _context;

        public MaterialRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Material material)
        {
            _context.Materials.Add(material);
            return Save();
        }

        public bool Delete(Material material)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Material>> GetAll()
        {
            return await _context.Materials.ToListAsync();
        }

        public Task<Material> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Material> GetByIdAsyncNoTrack(int id)
        {
            return await _context.Materials.AsNoTracking().FirstOrDefaultAsync(i => i.MaterialId == id);
        }

        public Task<IEnumerable<Material>> GetListByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Material material)
        {
            _context.Materials.Update(material);
            return Save();
        }
    }
}
