using InvoiceTool.Domain.Entities;
using InvoiceTool.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InvoiceTool.Infrastructure.Persistence.Repositories;

internal class CustomerRepository(AppDbContext context) : ICustomerRepository
{
    private readonly AppDbContext _context = context;

    public async Task<Customer?> GetAsync(int id)
    {
        return await _context.Customers.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Customer>> GetAllAsync()
    {
        var customers = await _context.Customers.AsNoTracking().ToListAsync();

        return customers ?? new List<Customer>();
    }

    public async Task<Customer> SaveAsync(Customer customer)
    {
        return customer.Id > 0 ? await UpdateAsync(customer) : await AddAsync(customer);
    }

    private async Task<Customer> AddAsync(Customer customer)
    {
        _context.Add(customer);

        await _context.SaveChangesAsync();

        return customer;
    }

    private async Task<Customer> UpdateAsync(Customer customer)
    {
        _context.Entry(customer).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        return customer;
    }

    public async Task<bool> DeleteAsync(int customerId)
    {
        var customer = await _context.Customers.FindAsync(customerId);

        if (customer == null)
            return false;


        _context.Customers.Remove(customer);

        var numberOfDeletedRecords = await _context.SaveChangesAsync();

        return numberOfDeletedRecords > 0;
    }
}
