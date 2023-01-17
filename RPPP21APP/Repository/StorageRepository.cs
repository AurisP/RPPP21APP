using Microsoft.EntityFrameworkCore;
using RPPP21APP.Data;
using RPPP21APP.Interfaces;
using RPPP21APP.Models;

namespace RPPP21APP.Repository
{
    public class StorageRepository : IStorageRepository
    {
        private readonly ApplicationDbContext _context;

        public StorageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Storage storage)
        {
            _context.Add(storage);
            return Save();
        }

        public bool Delete(Storage storage)
        {
            if (storage == null)
                return false;
            _context.Remove(storage);
            return Save();
        }

        public Task<IEnumerable<Storage>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Storage> GetByIdAsync(int id)
        {
            return await _context.Storages.FirstOrDefaultAsync(i => i.StorageId == id);
        }

        public Task<Storage> GetByIdAsyncNoTrack(int id)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Storage storage)
        {
            _context.Storages.Update(storage);
            return Save();
        }
    
    }
}
