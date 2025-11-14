using InvoiceTool.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceTool.Infrastructure.Persistence.Configurations;
class SettingsConfiguration : IEntityTypeConfiguration<Settings>
{
    public void Configure(EntityTypeBuilder<Settings> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedNever();

        builder.Property(c => c.CompanyName).HasMaxLength(100);
        builder.Property(c => c.CompanyEmail).HasMaxLength(100);
        builder.Property(c => c.CompanyAddress).HasMaxLength(100);
        builder.Property(c => c.CompanyPostalCode).HasMaxLength(50);
        builder.Property(c => c.CompanyCity).HasMaxLength(50);
        builder.Property(c => c.CompanyPhoneNumber).HasMaxLength(20);
        builder.Property(c => c.CompanyWebsite).HasMaxLength(100);
        builder.Property(c => c.CompanyCocNumber).HasMaxLength(50);
        builder.Property(c => c.CompanyVatNumber).HasMaxLength(50);
        builder.Property(c => c.CompanyIban).HasMaxLength(50);
        builder.Property(c => c.CompanyLogo);
        builder.Property(c => c.InvoiceBottomContent);


        builder.HasData(new Settings
        {
            Id = Guid.Parse("b9c6ed20-13e0-4bb1-b074-556d9fae7f19"),
            CompanyName = string.Empty,
            CompanyEmail = string.Empty,
            CompanyAddress = string.Empty,
            CompanyPostalCode = string.Empty,
            CompanyCity = string.Empty,
            CompanyPhoneNumber = string.Empty,
            CompanyWebsite = string.Empty,
            CompanyCocNumber = string.Empty,
            CompanyVatNumber = string.Empty,
            CompanyIban = string.Empty,
            CompanyLogo = string.Empty,
            InvoiceBottomContent = string.Empty
        });
    }
}