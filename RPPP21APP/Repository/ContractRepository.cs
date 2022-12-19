using Microsoft.EntityFrameworkCore;
using RPPP21APP.Data;
using RPPP21APP.Interfaces;
using RPPP21APP.Models;

namespace RPPP21APP.Repository
{
    public class ContractRepository : IContractRepository
    {
        private readonly ApplicationDbContext _context;

        public ContractRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Contract>> GetAll()
        {
            return await _context.Contracts.ToListAsync();
        }

        public async Task<Contract> GetByIdAsync(int id)
        {
            return await _context.Contracts.Include(i => i.Contractor).FirstOrDefaultAsync(i => i.ContractId == id);
        }

        public async Task<Contract> GetByIdAsyncNoTrack(int id)
        {
            return await _context.Contracts.Include(i => i.Contractor).AsNoTracking().FirstOrDefaultAsync(i => i.ContractId == id);
        }
        public bool Add(Contract contract)
        {
            _context.Add(contract);
            return Save();
        }

        public bool Delete(Contract contract)
        {
            _context.Remove(contract);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Contract contract)
        {
            _context.Update(contract);
            return Save();
        }
    }
}
