/// ----------------------------------------------------------------------
/// <summary>
/// Defines the Payment repository.
/// </summary>
/// ----------------------------------------------------------------------
namespace PaymentGatewayDemo.Repositories
{
    using Dapper;
    using Interfaces;
    using PaymentGatewayDemo.Enums;
    using PaymentGatewayDemo.Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    /// <summary>
    /// The Customer repository.
    /// </summary>
    public class PaymentRepository : BaseRepository, IPaymentRepository
    {
        /// <summary>
        /// Create Payment.
        /// </summary>
        public Guid CreatePayment(Payment payment)
        {
            using (IDbConnection db = this.Create())
            {
                var guid = Guid.NewGuid();
                string insertQuery = @"INSERT INTO [dbo].[Payment]
                                        ([PaymentCode]
                                        ,[PaymentStatusId]
                                        ,[Amount]
                                        ,[CurrencyId]
                                        ,[BankPaymentCode]
                                        ,[CreatedDate]
                                        ,[UpdatedDate])
                                        VALUES
                                        (
                                         @Guid
                                        ,@PaymentStatusId
                                        ,@Amount
                                        ,@CurrencyId
                                        ,NULL
                                        ,@CreatedDate
                                        ,GETDATE())";

                var result = db.Execute(insertQuery, new
                {
                    guid,
                    payment.PaymentStatusId,
                    payment.Amount,
                    payment.CurrencyId,
                    payment.CreatedDate
                });

                return guid;
            }
        }

        /// <summary>
        /// Update BankPaymentCode for Payment.
        /// </summary>
        public bool UpdateBankPaymentCode(Guid paymentCode, Guid bankPaymentCode)
        {
            using (IDbConnection db = this.Create())
            {
                var guid = Guid.NewGuid();
                string updateQuery = @"UPDATE Payment SET BankPaymentCode = @BankPaymentCode WHERE PaymentCode = @PaymentCode";

                var result = db.Execute(updateQuery, new
                {
                    bankPaymentCode,
                    paymentCode
                });

                return result > 0;
            }
        }

        /// <summary>
        /// Get Payment and Card Details by Payment Code.
        /// </summary>
        public PaymentWithCardDetails GetPaymentWithCardDetails(Guid paymentCode)
        {
            using (IDbConnection db = this.Create())
            {
                db.Open();

                var lookup = new Dictionary<Guid, PaymentWithCardDetails>();
                db.Query<Payment, PaymentCardDetails, PaymentWithCardDetails>(@"
                    SELECT p.*, cd.*
                    FROM Payment p
                    INNER JOIN [PaymentCardDetails] cd ON p.PaymentCode = cd.PaymentCode 
                    WHERE p.PaymentCode = @PaymentCode
                    ", (p, cd) =>
                {
                    PaymentWithCardDetails payment;
                    if (!lookup.TryGetValue(p.PaymentCode, out payment))
                    {
                        lookup.Add(p.PaymentCode, payment = new PaymentWithCardDetails()
                        {
                            PaymentCode = p.PaymentCode,
                            PaymentStatus = ((PaymentStatus)p.PaymentStatusId),
                            Amount = p.Amount,
                            Currency = ((Currency)p.CurrencyId),
                            BankTransactionCode = p.BankTransactionCode,
                            CreatedDate = p.CreatedDate,
                            UpdatedDate = p.UpdatedDate
                        });
                    }
                    if (payment.CardDetails == null)
                        payment.CardDetails = new PaymentCardDetails()
                        {
                            Id = cd.Id,
                            PaymentCode = cd.PaymentCode,
                            CardNumber = cd.CardNumber,
                            ExpiryMonth = cd.ExpiryMonth,
                            ExpiryYear = cd.ExpiryYear,
                            CVV = cd.CVV
                        };
                    return payment;
                }, param: new { PaymentCode = paymentCode }).AsQueryable();

                var resultList = lookup.Values;

                return resultList.FirstOrDefault();
            }
        }
    }
}