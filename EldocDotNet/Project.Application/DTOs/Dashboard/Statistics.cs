namespace Project.Application.DTOs.Dashboard
{
    public class Statistics
    {
        public int TotalUsers { get; set; }
        public int ThisMonthUsers { get; set; }
        public int LastMonthUsers { get; set; }
        public decimal UsersRatio { get; set; }

        public int TotalCards { get; set; }
        public int ThisMonthCards { get; set; }
        public int LastMonthCards { get; set; }
        public decimal CardsRatio { get; set; }

        public int TotalAgencies { get; set; }
        public int ThisMonthAgencies { get; set; }
        public int LastMonthAgencies { get; set; }
        public decimal AgenciesRatio { get; set; }

        public int TotalVisitors { get; set; }
        public int ThisMonthVisitors { get; set; }
        public int LastMonthVisitors { get; set; }
        public decimal VisitorsRatio { get; set; }

        public int TotalTransactionCount { get; set; }
        public int ThisMonthTransactionCount { get; set; }
        public int LastMonthTransactionCount { get; set; }
        public decimal TransactionCountRatio { get; set; }

        public decimal TotalTransactionAmount { get; set; }
        public decimal ThisMonthTransactionAmount { get; set; }
        public decimal LastMonthTransactionAmount { get; set; }
        public decimal TransactionAmountRatio { get; set; }
    }
}
