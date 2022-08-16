using Project.Application.DTOs.Payment;
using Project.Domain.Entities;

namespace Project.Application.Features.Interfaces
{
    public interface IPaymentService
    {
        Task<Payment> AddPayment(AddPayment addPayment);
    }
}
