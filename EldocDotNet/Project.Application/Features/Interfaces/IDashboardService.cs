using Project.Application.DTOs.Charts;
using Project.Application.DTOs.Dashboard;

namespace Project.Application.Features.Interfaces
{
    public interface IDashboardService
    {
        Task<Statistics> Statistics();
        Task<List<ColumnChartData>> TransactionsChart();
        Task<ColumnChartData> TransactionsChartPerCity();
        Task<TransactionsPieChart> TransactionsPieChart();
    }
}
