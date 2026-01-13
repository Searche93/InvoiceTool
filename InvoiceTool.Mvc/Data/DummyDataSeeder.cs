using InvoiceTool.Infrastructure.Persistence;

namespace InvoiceTool.Mvc.Data;

public static class DummyDataSeeder
{
    // Todo Create fake data seeding method

    public static void Seed(AppDbContext db)
    {
        //if (db.Customers.Any())
        //    return;

        //var random = new Random();

        //// Customers
        //var customers = Enumerable.Range(1, 10)
        //    .Select(i => new Customer
        //    {
        //        Name = $"Customer {i}",
        //        Email = $"customer{i}@test.com"
        //    })
        //    .ToList();

        //db.Customers.AddRange(customers);
        //db.SaveChanges();

        //// Invoices
        //foreach (var customer in customers)
        //{
        //    var invoiceCount = random.Next(1, 3); // totaal ±20

        //    for (int i = 0; i < invoiceCount; i++)
        //    {
        //        var invoice = new Invoice
        //        {
        //            CustomerId = customer.Id,
        //            InvoiceDate = DateTime.Today.AddDays(-random.Next(30))
        //        };

        //        invoice.Lines = Enumerable.Range(1, random.Next(2, 6))
        //            .Select(l => new InvoiceLine
        //            {
        //                Description = $"Line {l}",
        //                Quantity = random.Next(1, 5),
        //                UnitPrice = random.Next(10, 100)
        //            })
        //            .ToList();

        //        db.Invoices.Add(invoice);
        //    }
        //}

        //db.SaveChanges();
    }
}