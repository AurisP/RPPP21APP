using RPPP21APP.Models;

namespace RPPP21APP.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAll();
        Task<Customer> GetByIdAsync(int id);
        Task<Customer> GetByIdAsyncNoTrack(int id);
        Task<int> GetCountAsync();
        Task<IEnumerable<Customer>> GetSliceAsync(int offset, int size);
        bool Add(Customer customer);
        bool Update(Customer customer);
        bool Delete(Customer customer);
        bool Save();
    }
}
