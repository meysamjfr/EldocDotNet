using System.Collections.Generic;

namespace Project.Application.DTOs.Charts
{
    public class TransactionsPieChart
    {
        public List<string> Categories { get; set; } = new List<string>();
        public List<decimal> Amounts { get; set; } = new List<decimal>();
    }
}
