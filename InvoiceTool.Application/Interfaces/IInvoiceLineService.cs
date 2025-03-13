using InvoiceTool.Application.Models;

namespace InvoiceTool.Application.Interfaces
{
    public interface IInvoiceLineService
    {
        /// <summary>
        /// Get all invoice lines by <paramref name="invoiceId"/>
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns>InvoiceLine model</returns>
        Task<List<InvoiceLineModel>?> GetAllbyInvoiceAsync(int invoiceId);

        /// <summary>
        /// Save a InvoiceLine
        /// </summary>
        /// <param name="invoiceLineModel"></param>
        /// <returns>InvoiceLine model</returns>
        Task<InvoiceLineModel> SaveAsync(InvoiceLineModel invoiceLineModel);

        /// <summary>
        /// Delete a InvoiceLine by it's <paramref name="invoiceLineId"/>
        /// </summary>
        /// <param name="invoiceLineId"></param>
        /// <returns>True or False</returns>
        Task<bool> DeleteAsync(int invoiceLineId);
    }
}
