
using PaymentGatewayDemo.Models;
using System;
/// ----------------------------------------------------------------------
/// <summary>
/// Defines the Payment repository interface.
/// </summary>
/// ----------------------------------------------------------------------
namespace PaymentGatewayDemo.Repositories.Interfaces
{
    /// <summary>
    /// The Payment repository interface.
    /// </summary>
    public interface IPaymentRepository : IBaseRepository
    {
        /// <summary>
        /// Create Payment.
        /// </summary>
        Guid CreatePayment(Payment payment);

        /// <summary>
        /// Update BankPaymentCode for Payment.
        /// </summary>
        bool UpdateBankPaymentCode(Guid paymentCode, Guid bankPaymentCode);

        /// <summary>
        /// Get Payment and Card Details by Payment Code.
        /// </summary>
        PaymentWithCardDetails GetPaymentWithCardDetails(Guid paymentCode);
    }
}