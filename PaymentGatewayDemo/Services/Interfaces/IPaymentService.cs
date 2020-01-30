/// ----------------------------------------------------------------------
/// <summary>
/// Defines the IPaymentService interface.
/// </summary>
/// ----------------------------------------------------------------------

namespace PaymentGatewayDemo.Services.Interfaces
{
    using PaymentGatewayDemo.Models.ViewModels;
    using System;
    using System.Threading.Tasks;

    public interface IPaymentService
    {
        /// <summary>
        /// Create Payment.
        /// </summary>
        Task<PaymentResultViewModel> CreatePaymentAsync(PaymentCardDetailsInputViewModel paymentDetails);

        /// <summary>
        /// Get Payment.
        /// </summary>
        PaymentResultViewModel GetPayment(Guid paymentCode);
    }
}