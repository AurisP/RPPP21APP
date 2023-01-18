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
            return await _context.Contracts
                .Include(i => i.Contractor)
                .Include(i => i.Leases)
                    .ThenInclude(i => i.LeaseType)
                .AsNoTracking().ToListAsync();
        }

        public async Task<Contract> GetByIdAsync(int id)
        {
            return await _context.Contracts
                .Include(i => i.Contractor)
                .Include(i => i.Leases)
                    .ThenInclude(i => i.LeaseType)
                .AsNoTracking().FirstOrDefaultAsync(i => i.ContractId == id);
        }

        public async Task<Contract> GetByIdAsyncNoTrack(int id)
        {
            return await _context.Contracts
                .Include(i => i.Contractor)
                .Include(i => i.Leases)
                    .ThenInclude(i => i.LeaseType)
                .AsNoTracking().FirstOrDefaultAsync(i => i.ContractId == id);
        }
        public bool Add(Contract contract)
        {
            _context.Contracts.Add(contract);
            return Save();
        }

        public bool Delete(Contract contract)
        {
            _context.Contracts.Remove(contract);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }

        public bool Update(Contract contract)
        {
            _context.Contracts.Update(contract);
            return Save();
        }
    }
}
