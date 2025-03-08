using InvoiceTool.Domain.Entities;
using InvoiceTool.Domain.Interfaces;
using InvoiceTool.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InvoiceTool.Infrastructure.Repositories;

internal class CustomerRepository(AppDbContext context) : ICustomerRepository
{
    private readonly AppDbContext _context = context;

    public async Task<Customer?> GetAsync(int id)
    {
        return await _context.Customers.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Customer>> GetAllAsync()
    {
        var customers = await _context.Customers.AsNoTracking().ToListAsync<Customer>();

        return customers ?? new List<Customer>();
    }
}
