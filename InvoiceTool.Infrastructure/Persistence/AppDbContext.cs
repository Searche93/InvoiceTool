using InvoiceTool.Domain.Entities;
using InvoiceTool.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace InvoiceTool.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<InvoiceLine> InvoiceLines { get; set; }
    public DbSet<Customer> Customers { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new InvoiceConfiguration());
        modelBuilder.ApplyConfiguration(new InvoiceLineConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
    }
}