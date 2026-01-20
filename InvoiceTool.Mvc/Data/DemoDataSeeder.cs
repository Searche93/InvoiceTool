using Bogus;
using InvoiceTool.Domain.Entities;
using InvoiceTool.Domain.Enums;
using InvoiceTool.Infrastructure.Persistence;

namespace InvoiceTool.Mvc.Data;

public static class DemoDataSeeder
{
    private static readonly int MinAmount = 1;
    private static readonly int MaxAmount = 5;
    private static List<Customer> _customers = new();

    public static void Seed(AppDbContext db)
    {
        if (db.Customers.Any() || db.Invoices.Any())
            return;

        CreateFakeCustomers(db);
        CreateFakeInvoices(db);

        db.SaveChanges();
    }

    private static void CreateFakeCustomers(AppDbContext db)
    {
        var customerFaker = new Faker<Customer>("nl")
            .RuleFor(c => c.Name, f => f.Name.FullName())
            .RuleFor(c => c.CompanyName, f => f.Company.CompanyName())
            .RuleFor(c => c.Address, f => f.Address.StreetAddress())
            .RuleFor(c => c.PostalCode, f => f.Address.ZipCode("#### ??"))
            .RuleFor(c => c.City, f => f.Address.City());

        _customers = customerFaker.Generate(10);

        db.Customers.AddRange(_customers);

        db.SaveChanges();
    }

    private static void CreateFakeInvoices(AppDbContext db)
    {
        var invoiceNumber = 1;
        var invoiceFaker = new Faker<Invoice>("nl")
            .RuleFor(i => i.Date, f => f.Date.Past(2))
            .RuleFor(i => i.PaymentStatus, f => f.Random.Bool(0.7f) ? PaymentStatus.Completed : PaymentStatus.Pending)
            .RuleFor(i => i.Number, (f, i) => $"INV{i.Date.Year}{invoiceNumber++:000}")
            .RuleFor(i => i.InvoiceLines, (f, i) =>
            {
                var lines = new List<InvoiceLine>();
                var lineCount = f.Random.Int(2, 5);
                for (int l = 1; l <= lineCount; l++)
                {
                    lines.Add(new InvoiceLine
                    {
                        Description = f.Commerce.ProductName(),
                        Quantity = f.Random.Int(MinAmount, MaxAmount),
                        UnitPrice = f.Random.Decimal(50, 250),
                        TaxPercentage = 21,
                        Invoice = i
                    });
                }
                return lines;
            });


        var currentYear = DateTime.Today.Year;
        var currentMonth = DateTime.Today.Month;

        var startYear = currentYear - 2;

        for (int year = startYear; year <= currentYear; year++)
        {
            var maxMonth = (year == currentYear) ? currentMonth : 12;

            for (var month = 1; month <= maxMonth; month++)
            {
                var invoicesThisMonth = new Faker().Random.Int(MinAmount, MaxAmount);

                for (var j = 0; j < invoicesThisMonth; j++)
                {
                    var invoice = invoiceFaker.Generate();

                    var day = (year == currentYear && month == currentMonth)
                                ? new Random().Next(1, DateTime.Today.Day + 1)
                                : new Random().Next(1, DateTime.DaysInMonth(year, month) + 1);

                    invoice.Date = new DateTime(year, month, day);
                    invoice.ExpirationDate = invoice.Date.AddDays(30);
                    invoice.CustomerId = _customers[new Random().Next(_customers.Count)].Id;

                    CalculateTotals(invoice);

                    db.Invoices.Add(invoice);
                }
            }
        }
    }

    private static void CalculateTotals(Invoice invoice)
    {
        decimal net = 0;
        decimal tax = 0;

        foreach (var line in invoice.InvoiceLines!)
        {
            var lineNet = line.UnitPrice * line.Quantity;
            var lineTax = lineNet * (line.TaxPercentage / 100m);

            net += lineNet;
            tax += lineTax;
        }

        invoice.NetPrice = net;
        invoice.TaxPrice = tax;
        invoice.GrossPrice = net + tax;
    }
}
