using InvoiceTool.Domain.Entities;
using InvoiceTool.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InvoiceTool.Infrastructure.Persistence.Repositories;

internal class InvoiceLineRepository(AppDbContext context) : IInvoiceLineRepository
{
    private readonly AppDbContext _context = context;

    public async Task<List<InvoiceLine>> GetAllbyInvoiceAsync(int invoiceId)
    {
        var invoiceLines = await _context.InvoiceLines.Where(il => il.InvoiceId == invoiceId).AsNoTracking().ToListAsync();

        return invoiceLines ?? new List<InvoiceLine>();
    }

    public async Task<InvoiceLine> SaveAsync(InvoiceLine invoiceLine)
    {
        return invoiceLine.Id > 0 ? await UpdateAsync(invoiceLine) : await AddAsync(invoiceLine);
    }

    public async Task<bool> DeleteAsync(int invoiceLineId)
    {
        var invoiceLine = await _context.InvoiceLines.FindAsync(invoiceLineId);

        if (invoiceLine == null)
            return false;


        _context.InvoiceLines.Remove(invoiceLine);

        var numberOfDeletedRecords = await _context.SaveChangesAsync();

        return numberOfDeletedRecords > 0;
    }

    private async Task<InvoiceLine> AddAsync(InvoiceLine invoiceLine)
    {
        _context.Add(invoiceLine);

        await _context.SaveChangesAsync();

        return invoiceLine;
    }


    // Todo => still buggy.. needs to be fixed asap
    private async Task<InvoiceLine> UpdateAsync(InvoiceLine invoiceLine)
    {
        var existingLine = await _context.InvoiceLines.FindAsync(invoiceLine.Id);

        if (existingLine == null)
            return invoiceLine;

        _context.Entry(existingLine).CurrentValues.SetValues(invoiceLine);

        await _context.SaveChangesAsync();

        return existingLine;
    }

}