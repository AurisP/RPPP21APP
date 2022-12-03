using Microsoft.EntityFrameworkCore;
using RPPP21APP.Data;
using RPPP21APP.Interfaces;
using RPPP21APP.Models;

namespace RPPP21APP.Repository
{
    public class GroupOfPlantsRepository : IGroupOfPlants
    {
        private readonly ApplicationDbContext _context;
        public GroupOfPlantsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(GroupOfPlant groupOfPlant)
        {
            _context.Add(groupOfPlant);
            return Save();
        }

        public bool Delete(GroupOfPlant groupOfPlant)
        {
            _context.Remove(groupOfPlant);
            return Save();
        }

        public async Task<IEnumerable<GroupOfPlant>> GetAll()
        {
            return await _context.GroupOfPlants.Include(a => a.GroupsOnPlot).Include(a => a.PlantType).AsNoTracking().ToListAsync();
        }

        public async Task<GroupOfPlant> GetByIdAsync(int PlotId, int TypeId)
        {
            return await _context.GroupOfPlants.Include(i => i.GroupsOnPlot).Include(i => i.PlantType)
                .FirstOrDefaultAsync(i => i.GroupsOnPlotId == PlotId && i.PlantTypeId == TypeId);
        }

        public async Task<GroupOfPlant> GetByIdAsyncNoTrack(int PlotId, int TypeId)
        {
            return await _context.GroupOfPlants.Include(i => i.GroupsOnPlot).ThenInclude(i => i.Plot).Include(i => i.PlantType).AsNoTracking()
                .FirstOrDefaultAsync(i => i.GroupsOnPlot.PlotId == PlotId && i.PlantTypeId == TypeId);
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool Update(GroupOfPlant groupOfPlant)
        {
            throw new NotImplementedException();
        }
    }
}
