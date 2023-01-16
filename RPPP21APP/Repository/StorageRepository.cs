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
            _context.Remove(storage);
            return Save();
        }

        public Task<IEnumerable<Storage>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Storage> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
