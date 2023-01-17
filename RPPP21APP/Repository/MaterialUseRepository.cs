using Microsoft.EntityFrameworkCore;
using RPPP21APP.Data;
using RPPP21APP.Interfaces;
using RPPP21APP.Models;

namespace RPPP21APP.Repository
{
    public class MaterialUseRepository : IMaterialUseRepository
    {
        private readonly ApplicationDbContext _context;

        public MaterialUseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public int Add(MaterialUse materialUse)
        {
            _context.MaterialUses.Add(materialUse);
            return Save() == true? materialUse.MaterialUseId : -1;
            
        }

        public bool Delete(MaterialUse materialUse)
        {
            if(materialUse == null)
                return false;
            
            _context.MaterialUses.Remove(materialUse);
            return Save();
        }    
        
        public int Update(MaterialUse materialUse)
        {
            _context.MaterialUses.Update(materialUse);
            return Save() == true ? materialUse.MaterialUseId : -1;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<MaterialUse> GetByIdAsync(int? id)
        {
            return await _context.MaterialUses.Include(i => i.Material).FirstOrDefaultAsync(i => i.MaterialId == id);
        }

        public async Task<MaterialUse> GetByIdAsyncNoTrack(int? id)
        {
            return await _context.MaterialUses.AsNoTracking().FirstOrDefaultAsync(i => i.MaterialId == id);
        }

        public async Task<MaterialUse> GetByUseIdAsync(int? id)
        {
            return await _context.MaterialUses.Include(i => i.Material).FirstOrDefaultAsync(i => i.MaterialUseId == id);
        }
    }
}
