namespace Project.Application.DTOs.Expert
{
    public class ExpertDashboardStats
    {
        public decimal CardNetIncome { get; set; }
        public decimal TerminalNetIncome { get; set; }
        public int TotalUsers { get; set; }
        public int TotalCards { get; set; }
        public int TotalTransactions { get; set; }
    }
}
