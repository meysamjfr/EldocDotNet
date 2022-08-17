using Project.Application.DTOs.Datatable;
using Project.Application.DTOs.Datatable.Base;
using Project.Application.DTOs.Transaction;

namespace Project.Application.Features.Interfaces
{
    public interface ITransactionService
    {
        Task<bool> AddTransactionByAdmin(AddTransaction addTransaction);
        Task<double> Charge(int userId, double amount, string description);
        Task<DatatableResponse<TransactionDTO>> Datatable(TransactionDatatableInput input, FiltersFromRequestDataTable filtersFromRequest);
        Task<double> GetUserBalance(int userId);
        Task<double> PayChatWithExpertRequest(int userId, double amount, string description);
    }
}
