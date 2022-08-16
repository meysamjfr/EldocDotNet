namespace Project.Application.Features.Interfaces
{
    public interface ITransactionService
    {
        Task<double> Charge(int userId, double amount, string description);
        Task<double> GetUserBalance(int userId);
        Task<double> PayChatWithExpertRequest(int userId, double amount, string description);
    }
}
