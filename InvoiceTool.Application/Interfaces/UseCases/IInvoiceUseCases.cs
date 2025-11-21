using InvoiceTool.Application.UseCases.Invoices;

namespace InvoiceTool.Application.Interfaces.UseCases;
public interface IInvoiceUseCases
{
    ICalculateInvoice CalculateInvoice { get; }
    ICreateInvoice CreateInvoice { get; }
    ICreatePdfByteArray CreatePdfByteArray { get; }
    IDeleteInvoice DeleteInvoice { get; }
    IEditInvoice EditInvoice { get; }
    IGetAllInvoices GetAllInvoices { get; }
    IGetInvoiceById GetInvoiceById { get; }
    IGetInvoiceByInvoiceLineId GetInvoiceByInvoiceLineId { get; }
}
