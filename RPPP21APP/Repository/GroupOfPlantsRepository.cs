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
        public async Task<IEnumerable<GroupOfPlant>> GetAll()
        {
            return await _context.GroupOfPlants
                .Include(i => i.PlantType)
                .Include(i => i.GroupsOnPlot)
                    .ThenInclude(i => i.Plot)
                .Include(i => i.ActionOnGroups)
                    .ThenInclude(i => i.Action)
                .AsNoTracking()
                .ToListAsync();
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

        public async Task<IEnumerable<PlantType>> GetAvailableTypes(int plotId)
        {
            return await _context.GroupOfPlants
                .Include(i => i.GroupsOnPlot)
                .Where(g => g.GroupsOnPlot.PlotId != plotId)
                .Select(g => g.PlantType).ToListAsync();
                          
        }

        public async Task<GroupOfPlant?> GetByIdAsync(int plotId, int typeId)
        {
            return await _context.GroupOfPlants
                .Include(i => i.GroupsOnPlot)
                .Include(i => i.PlantType)

                .FirstOrDefaultAsync(i => i.GroupsOnPlotId == plotId && i.PlantTypeId == typeId);
        }

        public async Task<GroupOfPlant?> GetByIdAsyncNoTrack(int plotId, int typeId)
        {
            return await _context.GroupOfPlants
                .Include(i => i.GroupsOnPlot)
                    .ThenInclude(i => i.Plot)
                .Include(i => i.PlantType)
                .Include(i => i.Plants)

                .AsNoTracking().FirstOrDefaultAsync(i => i.GroupsOnPlot.PlotId == plotId && i.PlantTypeId == typeId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(GroupOfPlant groupOfPlant)
        {
            _context.Update(groupOfPlant);
            return Save();
        }
    }
}
