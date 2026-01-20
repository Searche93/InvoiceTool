using InvoiceTool.Domain.Interfaces;

namespace InvoiceTool.Application.UseCases.Statistics;

public interface ITotalSalesByMonth
{
    Task<decimal> Execute(int month);
}

public class TotalSalesByMonth(IStatsRepository statsRepository) : ITotalSalesByMonth
{
    private readonly IStatsRepository _statsRepository = statsRepository;

    public async Task<decimal> Execute(int month)
    {
        var data = await _statsRepository.TotalSalesByMonth(month);

        return data;
    }
}
