using InvoiceTool.Application.Models;
using InvoiceTool.Application.UseCases.Customers;
using InvoiceTool.Mvc.ViewModels.Customer;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceTool.Mvc.Controllers;

public class CustomerController(
    GetAllCustomers getAllCustomers,
    GetCustomerById getCustomerById,
    CreateCustomer createCustomer,
    EditCustomer editCustomer

) : Controller
{

    private readonly GetAllCustomers _getAllCustomers = getAllCustomers;
    private readonly GetCustomerById _getCustomerById = getCustomerById;
    private readonly CreateCustomer _createCustomer = createCustomer;
    private readonly EditCustomer _editCustomer = editCustomer;

    public async Task<IActionResult> IndexAsync()
    {
        var customers = await _getAllCustomers.Execute();

        var customerViewModel = new IndexCustomerViewModel { Customers = customers };

        return View(customerViewModel);
    }

    public async Task<IActionResult> ViewCustomer(int id)
    {
        var customer = await _getCustomerById.Execute(id);

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
            await _createCustomer.Execute(customer);

            return RedirectToAction(nameof(Index));
        }

        return View(customer);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var customer = await _getCustomerById.Execute(id);

        var editCustomerViewModel = new EditCustomerViewModel { Customer = customer };

        return View(editCustomerViewModel);
    }

    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Edit(CustomerModel customer)
    {
        if (ModelState.IsValid)
        {
            await _editCustomer.Execute(customer);

            return RedirectToAction(nameof(Index));
        }

        return View(customer);
    }
}
