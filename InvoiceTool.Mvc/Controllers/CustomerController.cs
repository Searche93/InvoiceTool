using InvoiceTool.Application.Interfaces.UseCases;
using InvoiceTool.Application.Models;
using InvoiceTool.Mvc.ViewModels.Customer;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceTool.Mvc.Controllers;

public class CustomerController(ICustomerUseCases customerUseCases) : Controller
{
    private readonly ICustomerUseCases _customerUseCases = customerUseCases;

    public async Task<IActionResult> IndexAsync()
    {
        var customers = await _customerUseCases.GetAllCustomers.ExecuteAsync();

        var customerViewModel = new IndexCustomerViewModel { Customers = customers };

        return View(customerViewModel);
    }

    public async Task<IActionResult> ViewCustomer(int id)
    {
        var customer = await _customerUseCases.GetCustomerById.ExecuteAsync(id);

        var customerViewModel = new ViewCustomerViewModel { Customer = customer };

        return View(customerViewModel);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Create(CustomerModel customer)
    {
        if (ModelState.IsValid)
        {
            await _customerUseCases.CreateCustomer.ExecuteAsync(customer);

            return RedirectToAction(nameof(Index));
        }

        return View(customer);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var customer = await _customerUseCases.GetCustomerById.ExecuteAsync(id);

        var editCustomerViewModel = new EditCustomerViewModel { Customer = customer };

        return View(editCustomerViewModel);
    }

    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Edit(CustomerModel customer)
    {
        if (ModelState.IsValid)
        {
            await _customerUseCases.EditCustomer.ExecuteAsync(customer);

            return RedirectToAction(nameof(Index));
        }

        return View(customer);
    }

    public async Task<bool> Delete(int id)
    {
        var isDeleted = await _customerUseCases.DeleteCustomer.ExecuteAsync(id);

        return isDeleted;
    }
}
