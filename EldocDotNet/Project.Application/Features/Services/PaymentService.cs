using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Parbad;
using Parbad.Gateway.ZarinPal;
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
        private readonly ITransactionRepository _transactionRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        private readonly IOnlinePayment _onlinePayment;
        private readonly IUserService _userService;

        public PaymentService(ITransactionRepository transactionRepository, IPaymentRepository paymentRepository, IMapper mapper, IOnlinePayment onlinePayment, IUserService userService)
        {
            _transactionRepository = transactionRepository;
            _paymentRepository = paymentRepository;
            _mapper = mapper;
            _onlinePayment = onlinePayment;
            _userService = userService;
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

            var newTransaction = new Transaction
            {
                UserId = find.UserId,
                PaymentType = find.PaymentType,
                Amount = find.Amount,
                Description = $"کد پرداخت: #{find.TrackingNumber}",
                TransactionType = TransactionType.Deposit,
            };

            await _transactionRepository.Add(newTransaction);

            await _paymentRepository.Update(find);

            switch (find.PaymentType)
            {
                case PaymentType.Charge:
                    var newBalance = await _transactionRepository.GetAllQueryable()
                        .Where(w => w.UserId == newTransaction.UserId && w.IsActive == true && w.PaymentType == PaymentType.Charge && w.TransactionType == TransactionType.Deposit)
                        .SumAsync(s => s.Amount);
                    await _userService.UpdateBalance((double)newBalance);
                    break;

                case PaymentType.ChatWithExpert:
                    //find.TypeActionId
                    break;

                default:
                    break;
            }

        }


        //public async Task<string> PayChatWithExpertRequest(int requestId, decimal amount)
        //{
        //    var payment = await _paymentRepository.GetAllQueryable()
        //        .FirstOrDefaultAsync(f => f.Token == $"chatwithexpertrequest-#{requestId}");

        //    if (payment == null)
        //    {
        //        var callbackUrl = "https://api.eldoc.ir/api/payment/verify";

        //        IPaymentRequestResult result = await _onlinePayment.RequestAsync(invoice =>
        //        {
        //            invoice
        //                .SetZarinPalData($"پرداخت فاکتور - درخواست مذاکره با کارشناس")
        //                .SetTrackingNumber(DateTime.Now.Ticks)
        //                .SetAmount(amount)
        //                .SetCallbackUrl(callbackUrl)
        //                .UseZarinPal();
        //        });

        //        if (result.IsSucceed)
        //        {
        //            payment = new Payment
        //            {
        //                Amount = amount,
        //                GatewayAccountName = result.GatewayAccountName,
        //                GatewayName = result.GatewayName,
        //                IsCompleted = false,
        //                Token = $"chatwithexpertrequest-#{requestId}",
        //                TransactionCode = Guid.NewGuid().ToString(),
        //                TrackingNumber = result.TrackingNumber,
        //            };

        //            await _paymentRepository.Add(payment);

        //            return result.GatewayTransporter.Descriptor.Url;
        //        }

        //        throw new BadRequestException($"خطای درگاه - {result.Message}");
        //    }

        //    if (payment.IsCompleted == true)
        //    {
        //        throw new BadRequestException("این درخواست از قبل تکمیل شده بود");
        //    }


        //    throw new BadRequestException("خطای نامشخص");
        //}

        //public async Task PayChatWithExpertRequestPayment(IPaymentFetchResult fetchResult)
        //{

        //}
    }
}
