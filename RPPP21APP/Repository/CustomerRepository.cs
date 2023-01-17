using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RPPP21APP.Data;
using RPPP21APP.Interfaces;
using RPPP21APP.Models;

namespace RPPP21APP.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;
        
        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {

            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<Customer> GetByIdAsyncNoTrack(int id)
        {
            return await _context.Customers.AsNoTracking().FirstOrDefaultAsync(w => w.CustomerId == id);
        }
        public async Task<int> GetCountAsync()
        {
            return await _context.Customers.CountAsync();
        }

        public async Task<IEnumerable<Customer>> GetSliceAsync(int offset, int size)
        {
            return await _context.Customers.Skip(offset).Take(size).ToListAsync();
        }
        public bool Add(Customer customer)
        {
            _context.Customers.Add(customer);
            return Save();
        }

        public bool Update(Customer customer)
        {
            _context.Customers.Update(customer);
            return Save();
        }

        public bool Delete(Customer customer)
        {
            _context.Customers.Remove(customer);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
