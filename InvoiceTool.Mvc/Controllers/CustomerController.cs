using InvoiceTool.Application.Interfaces;
using InvoiceTool.Mvc.ViewModels.Customer;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceTool.Mvc.Controllers
{
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
    }
}
