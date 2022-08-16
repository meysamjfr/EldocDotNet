using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Application.Contracts.Persistence;
using Project.Application.DTOs.Payment;
using Project.Application.Exceptions;
using Project.Application.Features.Interfaces;
using Project.Domain.Entities;
using Project.Domain.Enums;

namespace Project.Application.Features.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly ITransactionService _transactionService;

        public PaymentService(IPaymentRepository paymentRepository, IMapper mapper, IUserService userService, ITransactionService transactionService)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
            _userService = userService;
            _transactionService = transactionService;
        }

        public async Task AddCharge(AddPayment addPayment)
        {
            var model = _mapper.Map<Payment>(addPayment);

            model.PaymentType = PaymentType.Charge;

            await _paymentRepository.Add(model);
        }

        public async Task CheckTrackingNumber(long trackingNumber)
        {
            var find = await _paymentRepository.GetAllQueryable()
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.TrackingNumber == trackingNumber);

            if (find == null)
            {
                throw new BadRequestException("اطلاعات پرداخت پیدا نشد");
            }

            if (find.IsCompleted == true)
            {
                throw new BadRequestException("این درخواست از قبل تکمیل شده بود");
            }
        }

        public async Task VerifyPayment(long trackingNumber, string transactionCode)
        {
            var find = await _paymentRepository.GetAllQueryable()
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.TrackingNumber == trackingNumber);

            find.IsCompleted = true;
            find.TransactionCode = transactionCode;

            await _paymentRepository.Update(find);

            switch (find.PaymentType)
            {
                case PaymentType.Charge:
                    var newBalance = await _transactionService.Charge(find.UserId, find.Amount, $"کد پرداخت: #{find.TrackingNumber}");
                    await _userService.UpdateBalance((double)newBalance);
                    break;

                case PaymentType.ChatWithExpert:
                    //find.TypeActionId
                    break;

                default:
                    break;
            }

        }
    }
}
