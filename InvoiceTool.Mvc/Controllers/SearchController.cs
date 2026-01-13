using InvoiceTool.Application.Interfaces.UseCases;
using InvoiceTool.Application.Models;
using InvoiceTool.Application.UseCases.Customers;
using InvoiceTool.Domain.Enums;
using InvoiceTool.Mvc.ViewModels.Search;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceTool.Mvc.Controllers;

public class SearchController(ISearchUseCases searchUseCases) : Controller
{
    private readonly ISearchUseCases _searchUseCases = searchUseCases;


    public IActionResult SearchResults()
    {
        return View(new SearchResultsViewModel());
    }

    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> SearchResults(int entity, string searchInput)
    {
        var searchResultsViewModel = new SearchResultsViewModel();

        if (ModelState.IsValid)
        {
            searchResultsViewModel.SearchInput = searchInput;

            if (entity == (int)Entities.Unknown || entity == (int)Entities.Customer)
            {
                var customers = await _searchUseCases.SearchCustomer.Execute(searchInput);

                searchResultsViewModel.Customers = customers;
            }

            if (entity == (int)Entities.Unknown || entity == (int)Entities.Invoice)
            {
                var invoices = await _searchUseCases.SearchInvoices.Execute(searchInput);

                searchResultsViewModel.Invoices = invoices;
            }
        }

        return View(searchResultsViewModel);
    }
}
