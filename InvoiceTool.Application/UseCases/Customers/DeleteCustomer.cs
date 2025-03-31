﻿using InvoiceTool.Application.Interfaces;

namespace InvoiceTool.Application.UseCases.Customers;

public class DeleteCustomer(ICustomerService customerService)
{
    private readonly ICustomerService _customerService = customerService;

    public async Task<bool> Execute(int id)
    {
        var isDeleted = await _customerService.DeleteAsync(id);

        return isDeleted;
    }
}
