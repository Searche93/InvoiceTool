using InvoiceTool.Domain.Interfaces;

namespace InvoiceTool.Application.UseCases.Statistics;

public interface ITotalSalesByYear
{
    Task<decimal> Execute(int year);
}

public class TotalSalesByYear(IStatsRepository statsRepository) : ITotalSalesByYear
{
    private readonly IStatsRepository _statsRepository = statsRepository;

    public async Task<decimal> Execute(int year)
    {
        var data = await _statsRepository.TotalSalesByYear(year);

        return data;
    }
}
