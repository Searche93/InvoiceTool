using InvoiceTool.Domain.Entities;
using InvoiceTool.Domain.Enums;
using InvoiceTool.Infrastructure.Persistence;

namespace InvoiceTool.Mvc.Data;

public static class DemoDataSeeder
{
    private static readonly string[] FirstNames =
    {
        "Jan", "Piet", "Klaas", "Sanne", "Lisa", "Tom", "Emma", "Noah", "Lotte", "Daan"
    };

    private static readonly string[] LastNames =
    {
        "Jansen", "De Vries", "Bakker", "Visser", "Smit", "Meijer", "Bos", "Mulder"
    };

    private static readonly string[] CompanySuffixes =
    {
        "Solutions", "Consultancy", "Trading", "Group", "Services", "Logistics"
    };

    private static readonly string[] Streets =
    {
        "Dorpsstraat", "Kerklaan", "Stationsweg", "Hoofdstraat", "Industrieweg"
    };

    private static readonly string[] Cities =
    {
        "Amsterdam", "Utrecht", "Rotterdam", "Eindhoven", "Groningen"
    };

    public static void Seed(AppDbContext db)
    {
        if (db.Customers.Any())
            return;

        if (db.Invoices.Any())
            return;


        var random = new Random();

        var customers = Enumerable.Range(1, 10)
            .Select(_ =>
            {
                var first = FirstNames[random.Next(FirstNames.Length)];
                var last = LastNames[random.Next(LastNames.Length)];
                var street = Streets[random.Next(Streets.Length)];
                var city = Cities[random.Next(Cities.Length)];

                return new Customer
                {
                    Name = $"{first} {last}",
                    CompanyName = $"{last} {CompanySuffixes[random.Next(CompanySuffixes.Length)]}",
                    Address = $"{street} {random.Next(1, 200)}",
                    PostalCode = $"{random.Next(1000, 9999)} {((char)('A' + random.Next(26)))}{((char)('A' + random.Next(26)))}",
                    City = city
                };
            })
            .ToList();

        db.Customers.AddRange(customers);
        db.SaveChanges();


        SeedInvoicesForYear(db, customers, random, 2024, 20);
        SeedInvoicesForYear(db, customers, random, 2025, 20);

        db.SaveChanges();
    }

    private static void SeedInvoicesForYear(AppDbContext db, List<Customer> customers, Random random, int year, int invoiceCount)
    {
        var invoicesPerMonth = invoiceCount / 12;
        var remainder = invoiceCount % 12;

        int invoiceNumber = 1;

        for (int month = 1; month <= 12; month++)
        {
            var countThisMonth = invoicesPerMonth + (month <= remainder ? 1 : 0);

            for (int i = 0; i < countThisMonth; i++)
            {
                var date = new DateTime(
                    year,
                    month,
                    random.Next(1, DateTime.DaysInMonth(year, month) + 1));

                var isPaid = random.NextDouble() < 0.7;

                var invoice = new Invoice
                {
                    Number = $"INV{year}{invoiceNumber++:000}",
                    Date = date,
                    ExpirationDate = date.AddDays(30),
                    CustomerId = customers[random.Next(customers.Count)].Id,
                    PaymentStatus = isPaid
                        ? PaymentStatus.Completed
                        : PaymentStatus.Pending
                };

                invoice.InvoiceLines = CreateInvoiceLines(invoice, random);
                CalculateTotals(invoice);

                db.Invoices.Add(invoice);
            }
        }
    }

    private static List<InvoiceLine> CreateInvoiceLines(Invoice invoice, Random random)
    {
        var lines = new List<InvoiceLine>();
        var lineCount = random.Next(2, 6);

        for (int i = 1; i <= lineCount; i++)
        {
            lines.Add(new InvoiceLine
            {
                Description = $"Dienst {i}",
                Quantity = random.Next(1, 5),
                UnitPrice = random.Next(50, 250),
                TaxPercentage = 21,
                Invoice = invoice
            });
        }

        return lines;
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
