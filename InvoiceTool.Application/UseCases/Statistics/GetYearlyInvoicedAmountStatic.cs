using InvoiceTool.Application.UseCases.Statistics.Results;
using InvoiceTool.Domain.Interfaces;

namespace InvoiceTool.Application.UseCases.Statistics;

public interface IGetYearlyInvoicedAmountStatic
{
    Task<MonthlyRevenueResult> Execute();
}

public class GetYearlyInvoicedAmountStatic(IStatsRepository statsRepository) : IGetYearlyInvoicedAmountStatic
{
    private readonly IStatsRepository _statsRepository = statsRepository;

    public async Task<MonthlyRevenueResult> Execute()
    {
        var data = await _statsRepository.GetYearlyInvoicedAmountStatic();

        var years = data.Select(x => x.Year).Distinct().OrderBy(y => y);

        var result = new MonthlyRevenueResult();

        for (int month = 1; month <= 12; month++)
        {
            result.Labels.Add(new DateTime(2000, month, 1).ToString("MMMM"));
        }

        foreach (var year in years)
        {
            var values = new decimal[12];

            foreach (var month in Enumerable.Range(1, 12))
            {
                values[month - 1] = data
                    .FirstOrDefault(x => x.Year == year && x.Month == month)?.Total ?? 0;
            }

            result.YearValues[year] = values.ToList();
        }

        return result;
    }

}
