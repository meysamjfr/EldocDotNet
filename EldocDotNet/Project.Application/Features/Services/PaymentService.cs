using AutoMapper;
using Project.Application.Contracts.Persistence;
using Project.Application.DTOs.Payment;
using Project.Application.Features.Interfaces;
using Project.Domain.Entities;

namespace Project.Application.Features.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;

        public PaymentService(ITransactionRepository transactionRepository, IPaymentRepository paymentRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }

        public async Task<Payment> AddPayment(AddPayment addPayment)
        {
            var model = _mapper.Map<Payment>(addPayment);

            await _paymentRepository.Add(model);

            return model;
        }
    }
}
