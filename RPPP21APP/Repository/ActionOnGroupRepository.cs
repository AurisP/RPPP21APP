
using Microsoft.EntityFrameworkCore;
using RPPP21APP.Data;
using RPPP21APP.Interfaces;
using RPPP21APP.Models;

namespace RPPP21APP.Repository
{
    public class ActionOnGroupRepository : IActionOnGroupRepository
    {
        private readonly ApplicationDbContext _context;

        public ActionOnGroupRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(ActionOnGroup action)
        {
            _context.ActionOnGroups.Add(action);
            return Save();
        }

        public bool Delete(ActionOnGroup action)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ActionOnGroup>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ActionOnGroup> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionOnGroup> GetByIdAsyncNoTrack(int id)
        {
            return await _context.ActionOnGroups
                .Include(i => i.Worker)
                .Include(i => i.Action)
                .Include(i => i.MaterialUse)
                    .ThenInclude(i => i.Material)
                .Include(i => i.Storage)
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.ActionOnGroupId == id);
        }

        public async Task<IEnumerable<ActionOnGroup>> GetListByIdAsync(int id)
        {
            return await _context.ActionOnGroups
                .Include(i => i.Worker)
                .Include(i => i.Action)
                .Include(i => i.MaterialUse)
                    .ThenInclude(i =>i.Material)
                .Include(i => i.Storage)
                .AsNoTracking()
                .Where(i => i.GroupOfPlantsId == id).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(ActionOnGroup action)
        {
            throw new NotImplementedException();
        }
    }
}
