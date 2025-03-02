using InvoiceTool.Domain.Entities;
using InvoiceTool.Domain.Interfaces;
using InvoiceTool.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InvoiceTool.Infrastructure.Repositories;

internal class InvoiceRepository(AppDbContext context) : IInvoiceRepository
{
    private readonly AppDbContext _context = context;

    public async Task<Invoice?> Get(int id)
    {
        return await _context.Invoices.AsNoTracking().Include(i => i.InvoiceLines).FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<List<Invoice>> GetAll()
    {
        var result = await _context.Invoices.AsNoTracking().Include(i => i.InvoiceLines).ToListAsync<Invoice>();

        return result ?? new List<Invoice>();
    }
}