namespace InvoiceTool.Domain.Entities;
public class Settings
{
    public Guid Id { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string CompanyEmail { get; set; } = string.Empty;
    public string CompanyAddress { get; set; } = string.Empty;
    public string CompanyPostalCode { get; set; } = string.Empty;
    public string CompanyCity { get; set; } = string.Empty;
    public string CompanyPhoneNumber { get; set; } = string.Empty;
    public string CompanyWebsite { get; set; } = string.Empty;
    public string CompanyCocNumber { get; set; } = string.Empty;
    public string CompanyVatNumber { get; set; } = string.Empty;
    public string CompanyIban { get; set; } = string.Empty;
    public string CompanyLogo { get; set; } = string.Empty;
    public string InvoiceBottomContent { get; set; } = string.Empty;
}
