using DinkToPdf;
using DinkToPdf.Contracts;
using InvoiceTool.Application.Interfaces;

namespace InvoiceTool.Infrastructure.PdfLibrary;

public class PdfGenerator : IPdfGenerator
{
    private readonly IConverter _converter;

    public PdfGenerator()
    {
        NativeLibraryLoader.Load();
        _converter = new SynchronizedConverter(new PdfTools());
    }

    public byte[] GeneratePdf(string html, string? cssPath = null, string? documentTitle = null)
    {
        var globalSettings = new GlobalSettings
        {
            ColorMode = ColorMode.Color,
            Orientation = Orientation.Portrait,
            PaperSize = PaperKind.A4,
            Margins = new MarginSettings() { Top = 15, Bottom = 15, Left = 15, Right = 15 },
            DocumentTitle = !string.IsNullOrEmpty(documentTitle) ? documentTitle : "PDF Document",
            Out = null
        };

        var webSettings = new WebSettings
        {
            DefaultEncoding = "utf-8",
        };

        if (!string.IsNullOrEmpty(cssPath))
            webSettings.UserStyleSheet = cssPath;


        var objectSettings = new ObjectSettings
        {
            HtmlContent = html,
            WebSettings = webSettings
        };

        var pdf = new HtmlToPdfDocument()
        {
            GlobalSettings = globalSettings,
            Objects = { objectSettings }
        };

        var bytes = _converter.Convert(pdf);

        return bytes;
    }
}
