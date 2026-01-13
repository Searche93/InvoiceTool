using InvoiceTool.Application.Models;

namespace InvoiceTool.Application.Interfaces;

public interface IInvoiceService
{
    /// <summary>
    /// Get single invoice entity by it's <paramref name="id"/>
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Invoice model</returns>
    Task<InvoiceModel?> GetAsync(int id);

    /// <summary>
    /// Get the invoice based on the invoiceline using the <paramref name="invoiceLineId"/>
    /// </summary>
    /// <param name="invoiceLineId"></param>
    /// <returns>Invoice entity</returns>
    Task<InvoiceModel?> GetInvoiceByInvoiceLineIdAsync(int invoiceLineId);

    /// <summary>
    /// Get a list of invoices
    /// </summary>
    /// <returns>List of invoice model</returns>
    Task<List<InvoiceModel>> GetAllAsync();
    
    /// <summary>
    /// Save an invoice
    /// </summary>
    /// <param name="invoiceModel"></param>
    /// <returns><paramref name="invoiceModel"/></returns>
    Task<InvoiceModel> SaveAsync(InvoiceModel invoiceModel);

    /// <summary>
    /// Delete an invoice by it <paramref name="invoiceId"/>
    /// </summary>
    /// <param name="invoiceId"></param>
    /// <returns>True if deleted succesfully</returns>
    Task<bool> DeleteAsync(int invoiceId);

    /// <summary>
    /// Asynchronously searches for invoices that match the specified input criteria.
    /// </summary>
    /// <param name="searchInput">The search term or query used to filter invoices. This may include invoice numbers, customer names, or other
    /// relevant keywords. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of invoices matching the
    /// search criteria. The list will be empty if no invoices are found.</returns>
    Task<List<InvoiceModel>> SearchAsync(string searchInput);
}
