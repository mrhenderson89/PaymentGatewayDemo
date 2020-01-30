/// ----------------------------------------------------------------------
/// <summary>
/// Defines the PaymentWithCardDetails model.
/// </summary>
/// ----------------------------------------------------------------------

namespace PaymentGatewayDemo.Models
{
    using PaymentGatewayDemo.Enums;
    using System;

    public class PaymentWithCardDetails
    {
        /// <summary>
        /// The payment identifier
        /// </summary>
        public Guid PaymentCode { get; set; }

        /// <summary>
        /// The payment status Id
        /// </summary>
        public PaymentStatus PaymentStatus { get; set; }

        /// <summary>
        /// The amount
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// The currency Id
        /// </summary>
        public Currency Currency { get; set; }

        /// <summary>
        /// The bank payment identifier
        /// </summary>
        public string BankTransactionCode { get; set; }

        /// <summary>
        /// The created date
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// The updated date
        /// </summary>
        public DateTime UpdatedDate { get; set; }

        /// <summary>
        /// The card details
        /// </summary>
        public PaymentCardDetails CardDetails { get; set; }
    }
}