/// ----------------------------------------------------------------------
/// <summary>
/// Defines the Payment model.
/// </summary>
/// ----------------------------------------------------------------------

namespace PaymentGatewayDemo.Models
{
    using System;

    /// <summary>
    /// The payment database entity
    /// </summary>
    public class Payment
    {
        /// <summary>
        /// The identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The payment code
        /// </summary>
        public Guid PaymentCode { get; set; }

        /// <summary>
        /// The payment status Id
        /// </summary>
        public int PaymentStatusId { get; set; }

        /// <summary>
        /// The amount
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// The currency Id
        /// </summary>
        public int CurrencyId { get; set; }

        /// <summary>
        /// The bank payment code
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
    }
}