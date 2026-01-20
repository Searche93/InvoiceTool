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

    // Todo - Fix up this query, might be buggy. 
    public async Task<Invoice?> GetInvoiceByInvoiceLineIdAsync(int invoiceLineId)
    {
        return await _context.Invoices
            .Include(i => i.InvoiceLines)
            .Where(i => i.InvoiceLines.Any(il => il.Id == invoiceLineId))
            .FirstOrDefaultAsync();
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
        var existingInvoice = await _context.Invoices.FindAsync(invoice.Id);

        if (existingInvoice == null)
            return invoice;

        _context.Entry(existingInvoice).CurrentValues.SetValues(invoice);

        await _context.SaveChangesAsync();

        return existingInvoice;
    }

    public async Task<bool> DeleteAsync(int invoiceId)
    {
        var invoice = await _context.Invoices.FindAsync(invoiceId);

        if (invoice == null)
            return false;


        _context.Invoices.Remove(invoice);

        var numberOfDeletedRecords = await _context.SaveChangesAsync();

        return numberOfDeletedRecords > 0;
    }

    public async Task<List<Invoice>> SearchAsync(string searchInput)
    {
        var invoices = await _context.Invoices
            .AsNoTracking()
            .Where(i => i.Number.Contains(searchInput))
            .ToListAsync();

        return invoices ?? new List<Invoice>();
    }
}