using InvoiceTool.Domain.Entities;

namespace InvoiceTool.Domain.Interfaces;

public interface IInvoiceRepository
{
    /// <summary>
    /// Get single invoice entity by it's id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Invoice entity</returns>
    Task<Invoice?> GetAsync(int id);

    /// <summary>
    /// Get the invoice based on the invoiceline using the <paramref name="invoiceLineId"/>
    /// </summary>
    /// <param name="invoiceLineId"></param>
    /// <returns>Invoice entity</returns>
    Task<Invoice?> GetInvoiceByInvoiceLineIdAsync(int invoiceLineId);

    /// <summary>
    /// Get a list of invoices
    /// </summary>
    /// <returns>List of invoice entity</returns>
    Task<List<Invoice>> GetAllAsync();

    /// <summary>
    /// Add or update a invoice
    /// </summary>
    /// <param name="invoice"></param>
    /// <returns>Updated entity</returns>
    Task<Invoice> SaveAsync(Invoice invoice);

    /// <summary>
    /// Delete an invoice by it <paramref name="invoiceId"/>
    /// </summary>
    /// <param name="invoiceId"></param>
    /// <returns>True if deleted succesfully</returns>
    Task<bool> DeleteAsync(int invoiceId);
}
