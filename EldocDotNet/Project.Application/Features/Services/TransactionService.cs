using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Application.Contracts.Persistence;
using Project.Application.Features.Interfaces;
using Project.Domain.Entities;
using Project.Domain.Enums;

namespace Project.Application.Features.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IMapper _mapper;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(IMapper mapper, ITransactionRepository transactionRepository)
        {
            _mapper = mapper;
            _transactionRepository = transactionRepository;
        }

        public async Task<double> GetUserBalance(int userId)
        {
            return await _transactionRepository.GetAllQueryable()
                        .Where(w => w.UserId == userId && w.IsActive == true)
                        .Select(s => s.TransactionType == TransactionType.Deposit ? s.Amount : (s.Amount * (-1)))
                        .SumAsync();
        }

        public async Task<double> Charge(int userId, double amount, string description)
        {
            var newTransaction = new Transaction
            {
                UserId = userId,
                PaymentType = PaymentType.Charge,
                Amount = amount,
                Description = description,
                TransactionType = TransactionType.Deposit,
            };

            await _transactionRepository.Add(newTransaction);

            return await GetUserBalance(userId);
        }

        public async Task<double> PayChatWithExpertRequest(int userId, double amount, string description)
        {
            var newTransaction = new Transaction
            {
                UserId = userId,
                PaymentType = PaymentType.ChatWithExpert,
                Amount = amount,
                Description = description,
                TransactionType = TransactionType.Withdraw,
            };

            await _transactionRepository.Add(newTransaction);

            return await GetUserBalance(userId);
        }
    }
}
