using InvoiceTool.Domain.Entities;

namespace InvoiceTool.Domain.Interfaces;

public interface IInvoiceRepository
{
    /// <summary>
    /// Get single invoice entity by it's id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Invoice entity</returns>

    Task<Invoice?> Get(int id);

    /// <summary>
    /// Get a list of invoices
    /// </summary>
    /// <returns>List of invoice entity</returns>
    Task<List<Invoice>> GetAll();
}
