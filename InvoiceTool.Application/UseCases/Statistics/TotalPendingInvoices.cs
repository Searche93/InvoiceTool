using InvoiceTool.Domain.Interfaces;

namespace InvoiceTool.Application.UseCases.Statistics;

public interface ITotalPendingInvoices
{
    Task<int> Execute();
}

public class TotalPendingInvoices(IStatsRepository statsRepository) : ITotalPendingInvoices
{
    private readonly IStatsRepository _statsRepository = statsRepository;

    public async Task<int> Execute()
    {
        var data = await _statsRepository.TotalPendingInvoices();

        return data;
    }
}
