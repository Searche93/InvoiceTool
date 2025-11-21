using InvoiceTool.Application.Interfaces.UseCases;

namespace InvoiceTool.Application.UseCases.Invoices;
public class InvoiceUseCases(
        ICalculateInvoice calculateInvoice,
        ICreateInvoice createInvoice,
        ICreatePdfByteArray createPdfByteArray,
        IDeleteInvoice deleteInvoice,
        IEditInvoice editInvoice,
        IGetAllInvoices getAllInvoices,
        IGetInvoiceById getInvoiceById,
        IGetInvoiceByInvoiceLineId getInvoiceByInvoiceLineId
    ) : IInvoiceUseCases
{
    public ICalculateInvoice CalculateInvoice { get; } = calculateInvoice;

    public ICreateInvoice CreateInvoice { get; } = createInvoice;

    public ICreatePdfByteArray CreatePdfByteArray { get; } = createPdfByteArray;

    public IDeleteInvoice DeleteInvoice { get; } = deleteInvoice;

    public IEditInvoice EditInvoice { get; } = editInvoice;

    public IGetAllInvoices GetAllInvoices { get; } = getAllInvoices;

    public IGetInvoiceById GetInvoiceById { get; } = getInvoiceById;

    public IGetInvoiceByInvoiceLineId GetInvoiceByInvoiceLineId { get; } = getInvoiceByInvoiceLineId;
}
