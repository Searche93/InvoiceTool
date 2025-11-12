namespace InvoiceTool.Application.Interfaces;
public interface IPdfGenerator
{
    /// <summary>
    /// Generate a byte array from html string
    /// </summary>
    /// <param name="html"></param>
    /// <param name="cssPath"></param>
    /// <returns>Generate a byte array to save as PDF file.</returns>
    byte[] GeneratePdf(string html, string? cssPath = null);
}
