﻿using InvoiceTool.Application.Models;

namespace InvoiceTool.Mvc.ViewModels.Invoice;

public class CreateInvoiceViewModel
{
    public InvoiceModel Invoice { get; set; } = new InvoiceModel();
    public List<CustomerModel> Customers { get; set; } = new List<CustomerModel>();
}
