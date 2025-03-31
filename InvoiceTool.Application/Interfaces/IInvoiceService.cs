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
}
