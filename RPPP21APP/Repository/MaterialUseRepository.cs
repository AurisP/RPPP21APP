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
            throw new NotImplementedException();
        }    
        
        public bool Update(MaterialUse materialUse)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
