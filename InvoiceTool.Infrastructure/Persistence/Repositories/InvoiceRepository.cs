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

    public async Task<Invoice> SaveAsync(Invoice invoice)
    {
        return invoice.Id > 0 ? await UpdateAsync(invoice) : await AddAsync(invoice);
    }

    private async Task<Invoice> AddAsync(Invoice invoice)
    {
        _context.Add(invoice);

        await _context.SaveChangesAsync();

        return invoice;
    }

    private async Task<Invoice> UpdateAsync(Invoice invoice)
    {
        _context.Entry(invoice).State = EntityState.Modified;

        if(invoice.InvoiceLines != null)
        {
            foreach (var line in invoice.InvoiceLines)
            {
                var state = line.Id > 0 ? EntityState.Modified : EntityState.Added;

                _context.Entry(line).State = state;
            }
        }

        await _context.SaveChangesAsync();

        return invoice;
    }
}