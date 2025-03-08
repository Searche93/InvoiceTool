using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.Models;
using InvoiceTool.Mvc.ViewModels.Customer;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceTool.Mvc.Controllers;

public class CustomerController(ICustomerService customerService) : Controller
{
    private readonly ICustomerService _customerService = customerService;

    public async Task<IActionResult> IndexAsync()
    {
        var customers = await _customerService.GetAllAsync();

        var customerViewModel = new IndexCustomerViewModel
        {
            Customers = customers
        };

        return View(customerViewModel);
    }

    public async Task<IActionResult> ViewCustomer(int id)
    {
        var customer = await _customerService.GetAsync(id);

        var customerViewModel = new ViewCustomerViewModel
        {
            Customer = customer
        };

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
            await _customerService.SaveAsync(customer);

            return RedirectToAction(nameof(Index));
        }

        return View(customer);
    }

    public async Task <IActionResult> Edit(int id)
    {
        var customer = await _customerService.GetAsync(id);

        var editCustomerViewModel = new EditCustomerViewModel
        {
            Customer = customer
        };

        return View(editCustomerViewModel);
    }

    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Edit(CustomerModel customer)
    {
        if (ModelState.IsValid)
        {
            await _customerService.SaveAsync(customer);

            return RedirectToAction(nameof(Index));
        }

        return View(customer);
    }
}
