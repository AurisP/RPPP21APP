using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RPPP21APP.Data;
using RPPP21APP.Interfaces;
using RPPP21APP.Models;

namespace RPPP21APP.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ApplicationDbContext _context;
        
        public ReservationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reservation>> GetAll()
        {

            return await _context.Reservations.ToListAsync();
        }

        public async Task<Reservation> GetByIdAsync(int id)
        {
            return await _context.Reservations.FindAsync(id);
        }

        public async Task<Reservation> GetByIdAsyncNoTrack(int id)
        {
            return await _context.Reservations.AsNoTracking().FirstOrDefaultAsync(w => w.ReservationId == id);
        }
        public async Task<int> GetCountAsync()
        {
            return await _context.Reservations.CountAsync();
        }

        public async Task<IEnumerable<Reservation>> GetSliceAsync(int offset, int size)
        {
            return await _context.Reservations.Skip(offset).Take(size).ToListAsync();
        }
        public bool Add(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            return Save();
        }

        public bool Update(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
            return Save();
        }

        public bool Delete(Reservation reservation)
        {
            _context.Reservations.Remove(reservation);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
