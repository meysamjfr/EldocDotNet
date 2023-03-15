using DNTPersianUtils.Core;
using Microsoft.EntityFrameworkCore;
using Project.Application.Contracts.Persistence;
using Project.Application.DTOs.Charts;
using Project.Application.DTOs.Dashboard;
using Project.Application.Features.Interfaces;

namespace Project.Application.Features.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IUserRepository _userRepository;

        public DashboardService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Statistics> Statistics()
        {
            var rnd = new Random();

            var res = new Statistics();

            var thisMonthDate = DateTime.Now.GetPersianMonthStartAndEndDates(true).StartDate.Date;
            var lastMonthDate = DateTime.Now.AddMonths(-1).GetPersianMonthStartAndEndDates(true).StartDate.Date;


            var allUsers = _userRepository
                .GetAllQueryable()
                .Where(w => w.IsActive == true)
                .AsNoTracking();
            decimal thisMonthUsers = await allUsers.Where(w => w.CreatedAt.Date >= thisMonthDate).CountAsync();
            decimal lastMonthUsers = await allUsers.Where(w => w.CreatedAt.Date >= lastMonthDate && w.CreatedAt < thisMonthDate).CountAsync();
            res.TotalUsers = await allUsers.CountAsync();
            if (thisMonthUsers > 0)
            {
                res.UsersRatio = lastMonthUsers > 0 ? (thisMonthUsers - lastMonthUsers) / lastMonthUsers * 100 : thisMonthUsers * 100;
            }
            res.ThisMonthUsers = (int)thisMonthUsers;
            res.LastMonthUsers = (int)lastMonthUsers;


            //var allCards = _userCardRepository
            //    .GetAllQueryable()
            //    .Where(w => w.IsActive == true)
            //    .AsNoTracking();
            //decimal thisMonthCards = await allCards.Where(w => w.CreatedAt.Date >= thisMonthDate).CountAsync();
            //decimal lastMonthCards = await allCards.Where(w => w.CreatedAt.Date >= lastMonthDate && w.CreatedAt < thisMonthDate).CountAsync();
            //res.TotalCards = await allCards.CountAsync();
            //if (thisMonthCards > 0)
            //{
            //    res.CardsRatio = lastMonthCards > 0 ? (thisMonthCards - lastMonthCards) / lastMonthCards * 100 : thisMonthCards * 100;
            //}
            //res.ThisMonthCards = (int)thisMonthCards;
            //res.LastMonthCards = (int)lastMonthCards;


            //var allAgencies = _refferRepository
            //    .GetAllQueryable()
            //    .Where(w => w.IsActive == true)
            //    .Where(w => w.AgencyId == null)
            //    .AsNoTracking();
            //decimal thisMonthAgencies = await allAgencies.Where(w => w.CreatedAt.Date >= thisMonthDate).CountAsync();
            //decimal lastMonthAgencies = await allAgencies.Where(w => w.CreatedAt.Date >= lastMonthDate && w.CreatedAt < thisMonthDate).CountAsync();
            //res.TotalAgencies = await allAgencies.CountAsync();
            //if (thisMonthAgencies > 0)
            //{
            //    res.AgenciesRatio = lastMonthAgencies > 0 ? (thisMonthAgencies - lastMonthAgencies) / lastMonthAgencies * 100 : thisMonthAgencies * 100;
            //}
            //res.ThisMonthAgencies = (int)thisMonthAgencies;
            //res.LastMonthAgencies = (int)lastMonthAgencies;


            //var allVisitors = _refferRepository
            //    .GetAllQueryable()
            //    .Where(w => w.IsActive == true)
            //    .Where(w => w.AgencyId != null)
            //    .AsNoTracking();
            //decimal thisMonthVisitors = await allVisitors.Where(w => w.CreatedAt.Date >= thisMonthDate).CountAsync();
            //decimal lastMonthVisitors = await allVisitors.Where(w => w.CreatedAt.Date >= lastMonthDate && w.CreatedAt < thisMonthDate).CountAsync();
            //res.TotalVisitors = await allVisitors.CountAsync();
            //if (thisMonthVisitors > 0)
            //{
            //    res.VisitorsRatio = (int)(lastMonthVisitors > 0 ? (thisMonthVisitors - lastMonthVisitors) / lastMonthVisitors * 100 : thisMonthVisitors * 100);
            //}
            //res.ThisMonthVisitors = (int)thisMonthVisitors;
            //res.LastMonthVisitors = (int)lastMonthVisitors;


            //var allTransactionCount = _cardTransactionRepository
            //    .GetAllQueryable()
            //    .Where(w => w.IsActive == true)
            //    .AsNoTracking();
            //decimal thisMonthTransactionCount = await allTransactionCount.Where(w => w.LocalDateTime.Date >= thisMonthDate).CountAsync();
            //decimal lastMonthTransactionCount = await allTransactionCount.Where(w => w.LocalDateTime.Date >= lastMonthDate && w.LocalDateTime < thisMonthDate).CountAsync();
            //res.TotalTransactionCount = await allTransactionCount.CountAsync();
            //if (thisMonthTransactionCount > 0)
            //{
            //    res.TransactionCountRatio = (int)(lastMonthTransactionCount > 0 ? (thisMonthTransactionCount - lastMonthTransactionCount) / lastMonthTransactionCount * 100 : thisMonthTransactionCount * 100);
            //}
            //res.ThisMonthTransactionCount = (int)thisMonthTransactionCount;
            //res.LastMonthTransactionCount = (int)lastMonthTransactionCount;


            //var allTransactionAmount = _cardTransactionRepository
            //    .GetAllQueryable()
            //    .Where(w => w.IsActive == true)
            //    .AsNoTracking();
            //decimal thisMonthTransactionAmount = await allTransactionAmount.Where(w => w.LocalDateTime.Date >= thisMonthDate).SumAsync(s => s.Amount);
            //decimal lastMonthTransactionAmount = await allTransactionAmount.Where(w => w.LocalDateTime.Date >= lastMonthDate && w.LocalDateTime < thisMonthDate).SumAsync(s => s.Amount);
            //res.TotalTransactionAmount = await allTransactionAmount.SumAsync(s => s.Amount);
            //if (thisMonthTransactionAmount > 0)
            //{
            //    res.TransactionAmountRatio = lastMonthTransactionAmount > 0 ? (thisMonthTransactionAmount - lastMonthTransactionAmount) / lastMonthTransactionAmount * 100 : thisMonthTransactionAmount * 100;
            //}
            //res.ThisMonthTransactionAmount = thisMonthTransactionAmount;
            //res.LastMonthTransactionAmount = lastMonthTransactionAmount;

            return res;
        }

        public List<ColumnChartData> TransactionsChart()
        {
            var resList = new List<ColumnChartData>
            {
                new ColumnChartData(),
                new ColumnChartData()
            };

            //var thisMonthDate = DateTime.Now.GetPersianMonthStartAndEndDates(true).StartDate.Date;

            //var cardTransactionData = await _cardTransactionRepository
            //    .GetAllQueryable()
            //    .Where(w => w.IsActive == true)
            //    .ToListAsync();

            //var date = DateTime.Now.GetPersianMonthStartAndEndDates(true);

            //for (int i = 0; i < 6; i++)
            //{
            //    date = date.StartDate.AddDays(-i).GetPersianMonthStartAndEndDates(true);

            //    resList[0].ColumnName.Add(date.StartDate.ToPersianDateTimeString("MMMM"));
            //    resList[1].ColumnName.Add(date.StartDate.ToPersianDateTimeString("MMMM"));

            //    var netIncome = cardTransactionData
            //        .Where(w => w.LocalDateTime.Ticks >= date.StartDate.Ticks && w.LocalDateTime.Ticks <= date.EndDate.Ticks)
            //        .Sum(c => c.NetIncome);

            //    var barmanDiscountPrice = cardTransactionData
            //        .Where(w => w.LocalDateTime.Ticks >= date.StartDate.Ticks && w.LocalDateTime.Ticks <= date.EndDate.Ticks)
            //        .Sum(c => c.BarmanDiscountPrice);

            //    resList[0].ColumnValue.Add(netIncome);
            //    resList[1].ColumnValue.Add(barmanDiscountPrice);
            //}

            return resList;
        }

        public TransactionsPieChart TransactionsPieChart()
        {
            var result = new TransactionsPieChart();

            //var categories = await _storeCategoryRepository.GetAllQueryable()
            //    .AsNoTracking()
            //    .Where(w => w.IsActive == true && w.ParentId == null)
            //    .ToListAsync();

            //var data = _cardTransactionRepository.GetAllQueryable()
            //    .AsNoTracking()
            //    .Where(w => w.IsActive == true)
            //    .AsEnumerable()
            //    .GroupBy(g => g.MainCategoryId)
            //    .Select(s => new { gid = s.Key, data = s.FirstOrDefault() })
            //    .ToList();

            //foreach (var category in categories)
            //{
            //    result.Categories.Add(category.Name);
            //    result.Amounts.Add(data.Where(w => w.gid == category.Id).Sum(s => s.data.BarmanDiscountPrice));
            //}

            return result;
        }

        public ColumnChartData TransactionsChartPerCity()
        {
            var result = new ColumnChartData();

            //var data = await _cardTransactionRepository.GetAllQueryable()
            //    .Where(w => w.IsActive)
            //    .GroupBy(g => g.City)
            //    .Select(s => new
            //    {
            //        gid = s.Key,
            //        barman = s.Sum(s2 => s2.BarmanDiscountPrice),
            //    })
            //    .ToListAsync();

            //foreach (var item in data)
            //{
            //    result.ColumnName.Add(item.gid);
            //    result.ColumnValue.Add(item.barman);
            //}


            return result;
        }

    }
}
