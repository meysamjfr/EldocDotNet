using Project.Application.DTOs.Charts;
using Project.Application.DTOs.Dashboard;

namespace Project.Application.Features.Interfaces
{
    public interface IDashboardService
    {
        Task<Statistics> Statistics();
        List<ColumnChartData> TransactionsChart();
        ColumnChartData TransactionsChartPerCity();
        TransactionsPieChart TransactionsPieChart();
    }
}
