using InvoiceTool.Application.Interfaces;

namespace InvoiceTool.Application.UseCases.Invoices;
public class CreatePdfByteArray
{
    private readonly IPdfGenerator _pdf;

    public CreatePdfByteArray(IPdfGenerator pdf)
    {
        _pdf = pdf;
    }
    public byte[] Execute(string html, string? cssPath = null)
    {
        var pdfBytes = _pdf.GeneratePdf(html, cssPath);

        return pdfBytes;
    }
}
