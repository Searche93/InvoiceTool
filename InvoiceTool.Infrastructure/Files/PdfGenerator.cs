using DocumentTools.Pdf;
using InvoiceTool.Application.Interfaces;

namespace InvoiceTool.Infrastructure.Files;
public class PdfGenerator : IPdfGenerator
{
    public byte[] GeneratePdf(string html, string? cssPath = null)
    {
        var pdfOptions = new PdfOptions();

        if(!string.IsNullOrEmpty(cssPath))
        {
            pdfOptions.UserStyleSheet = cssPath;
        }

        var pdf = new Pdf(pdfOptions);

        var bytes = pdf.CreatePdf(html);

        return bytes;
    }
}
