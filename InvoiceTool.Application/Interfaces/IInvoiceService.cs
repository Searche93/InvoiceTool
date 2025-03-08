using InvoiceTool.Application.Models;

namespace InvoiceTool.Application.Interfaces;

public interface IInvoiceService
{
    /// <summary>
    /// Get single invoice entity by it's id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Invoice model</returns>

    Task<InvoiceModel?> GetAsync(int id);

    /// <summary>
    /// Get a list of invoices
    /// </summary>
    /// <returns>List of invoice model</returns>
    Task<List<InvoiceModel>> GetAllAsync();
}
