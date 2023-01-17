using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RPPP21APP.Data;
using RPPP21APP.Interfaces;
using RPPP21APP.Models;

namespace RPPP21APP.Repositories
{
    public class PlantReservationRepository : IPlantReservationRepository
    {
        private readonly ApplicationDbContext _context;

        public PlantReservationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PlantsReservation>> GetAll()
        {

            return await _context.PlantsReservations.ToListAsync();
        }

        public async Task<PlantsReservation> GetByIdAsync(int id)
        {
            return await _context.PlantsReservations.FindAsync(id);
        }

        public async Task<PlantsReservation> GetByIdAsyncNoTrack(int id)
        {
            return await _context.PlantsReservations.AsNoTracking().FirstOrDefaultAsync(w => w.PlantsReservationId == id);
        }
        public async Task<int> GetCountAsync()
        {
            return await _context.PlantsReservations.CountAsync();
        }

        public async Task<IEnumerable<PlantsReservation>> GetSliceAsync(int offset, int size)
        {
            return await _context.PlantsReservations.Skip(offset).Take(size).ToListAsync();
        }
        public bool Add(PlantsReservation plantsReservation)
        {
            _context.PlantsReservations.Add(plantsReservation);
            return Save();
        }

        public bool Update(PlantsReservation plantsReservation)
        {
            _context.PlantsReservations.Update(plantsReservation);
            return Save();
        }

        public bool Delete(PlantsReservation plantsReservation)
        {
            _context.PlantsReservations.Remove(plantsReservation);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
