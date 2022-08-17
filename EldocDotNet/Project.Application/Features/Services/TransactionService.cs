using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Application.Contracts.Persistence;
using Project.Application.DTOs.Datatable;
using Project.Application.DTOs.Datatable.Base;
using Project.Application.DTOs.Transaction;
using Project.Application.Extensions;
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

        public async Task<DatatableResponse<TransactionDTO>> Datatable(TransactionDatatableInput input, FiltersFromRequestDataTable filtersFromRequest)
        {
            var data = _transactionRepository.GetAllQueryable()
                .Include(i => i.User)
                .Where(w => w.IsActive == input.IsActive);

            var totalRecords = await data.CountAsync();

            if (!string.IsNullOrWhiteSpace(filtersFromRequest.SearchValue))
            {
                data = data.Where(w =>
                    w.User.Nickname.Contains(filtersFromRequest.SearchValue) ||
                    w.Description.Contains(filtersFromRequest.SearchValue) ||
                    w.Amount.ToString().Contains(filtersFromRequest.SearchValue)
                );
            }

            if (input.Id.HasValue && input.Id.Value > 0)
            {
                data = data.Where(w => w.Id == input.Id);
            }

            if (input.UserId.HasValue && input.UserId.Value > 0)
            {
                data = data.Where(w => w.UserId == input.Id);
            }

            return await data.ToDataTableAsync<Transaction, TransactionDTO>(totalRecords, filtersFromRequest, _mapper);
        }

        public async Task<bool> AddTransactionByAdmin(AddTransaction addTransaction)
        {
            var model = _mapper.Map<Transaction>(addTransaction);

            await _transactionRepository.Add(model);

            return true;
        }

    }
}
