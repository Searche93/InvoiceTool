using InvoiceTool.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceTool.Infrastructure.Persistence.Configurations
{
    class InvoiceLineConfiguration : IEntityTypeConfiguration<InvoiceLine>
    {
        public void Configure(EntityTypeBuilder<InvoiceLine> builder)
        {
            builder.HasKey(l => l.Id);

            builder.Property(l => l.Description)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(l => l.Quantity)
                .IsRequired();

            builder.Property(l => l.UnitPrice)
                .IsRequired();

            builder.HasOne(l => l.Invoice)
                .WithMany(i => i.InvoiceLines)
                .HasForeignKey(l => l.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
