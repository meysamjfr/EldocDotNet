﻿using System.Collections.Generic;

namespace Project.Application.DTOs.Charts
{
    public class ColumnChartData
    {
        public List<string> ColumnName { get; set; } = new List<string>();
        public List<decimal> ColumnValue { get; set; } = new List<decimal>();
    }
}
