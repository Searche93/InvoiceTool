using InvoiceTool.Domain.Entities;

namespace InvoiceTool.Domain.Interfaces;

public interface IInvoiceLineRepository
{
    /// <summary>
    /// Get all invoice lines by <paramref name="invoiceId"/>
    /// </summary>
    /// <param name="invoiceId"></param>
    /// <returns></returns>
    Task<List<InvoiceLine>> GetAllbyInvoiceAsync(int invoiceId);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="invoiceLine"></param>
    /// <returns></returns>
    Task<InvoiceLine> SaveAsync(InvoiceLine invoiceLine);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="invoiceLineId"></param>
    /// <returns></returns>
    Task<bool> DeleteAsync(int invoiceLineId);
}
