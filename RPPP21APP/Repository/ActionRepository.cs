using Microsoft.EntityFrameworkCore;
using RPPP21APP.Data;
using RPPP21APP.Interfaces;
using RPPP21APP.Models;
using System.Diagnostics.Contracts;

namespace RPPP21APP.Repository
{
    public class ActionRepository : IActionRepository
    {
        private readonly ApplicationDbContext _context;

        public ActionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(ActionM action)
        {
            _context.Actions.Add(action);
            return Save();
        }

        public bool Delete(ActionM action)
        {
            _context.Remove(action);
            return Save();
        }

        public async Task<ActionM> GetByIdAsync(int id)
        {
            return await _context.Actions.FirstOrDefaultAsync(i => i.ActionId == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(ActionM action)
        {
            _context.Update(action);
            return Save();
        }
    }
}
