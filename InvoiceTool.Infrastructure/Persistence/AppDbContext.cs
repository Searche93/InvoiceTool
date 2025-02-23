using InvoiceTool.Domain.Entities;
using InvoiceTool.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace InvoiceTool.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<InvoiceLine> InvoiceLines { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new InvoiceConfiguration());
    }
}
