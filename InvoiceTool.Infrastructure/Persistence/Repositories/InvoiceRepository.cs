using InvoiceTool.Domain.Entities;
using InvoiceTool.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InvoiceTool.Infrastructure.Persistence.Repositories;

internal class InvoiceRepository(AppDbContext context) : IInvoiceRepository
{
    private readonly AppDbContext _context = context;

    public async Task<Invoice?> GetAsync(int id)
    {
        return await _context.Invoices.AsNoTracking().Include(i => i.InvoiceLines).FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<List<Invoice>> GetAllAsync()
    {
        var invoices = await _context.Invoices.AsNoTracking().ToListAsync();

        return invoices ?? new List<Invoice>();
    }
}