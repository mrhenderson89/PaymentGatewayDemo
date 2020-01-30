/// ----------------------------------------------------------------------
/// <summary>
/// Defines the PaymentCardDetails model.
/// </summary>
/// ----------------------------------------------------------------------

namespace PaymentGatewayDemo.Models
{
    using System;

    /// <summary>
    /// The paymentcarddetails database entity
    /// </summary>
    public class PaymentCardDetails
    {
        /// <summary>
        /// The paymentcarddetails identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The payment code
        /// </summary>
        public Guid PaymentCode { get; set; }

        /// <summary>
        /// The card number
        /// </summary>
        public int CardNumber { get; set; }

        /// <summary>
        /// The card expiry month
        /// </summary>
        public int ExpiryMonth { get; set; }

        /// <summary>
        /// The card expiry year
        /// </summary>
        public int ExpiryYear { get; set; }

        /// <summary>
        /// The CVV
        /// </summary>
        public int CVV { get; set; }
    }
}