using Project.Application.DTOs.Payment;
using Project.Domain.Entities;

namespace Project.Application.Features.Interfaces
{
    public interface IPaymentService
    {
        Task AddCharge(AddPayment addPayment);
        Task CheckTrackingNumber(long trackingNumber);
        Task VerifyPayment(long trackingNumber, string transactionCode);
    }
}
