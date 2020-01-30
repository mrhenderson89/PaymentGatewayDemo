using PaymentGatewayDemo.Enums;
using PaymentGatewayDemo.Models.ViewModels;
using PaymentGatewayDemo.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;

namespace PaymentGatewayDemo.Controllers
{
    /// <summary>
    /// Represents the Customer controller.
    /// </summary>
    [RoutePrefix("api/v1/Payments")]
    public class PaymentController : ApiController
    {
        #region Fields

        /// <summary>
        /// the Order Repository.
        /// </summary>
        private readonly IPaymentService paymentService;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialises the Order controller.
        /// </summary>
        public PaymentController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds an Order
        /// </summary>
        // POST: /Orders
        [Route("")]
        [HttpPost]
        public HttpResponseMessage AddPayment(PaymentCardDetailsInputViewModel payment)
        {
            var cardNumberRegex = new Regex("^[0-9]+$");

            if (payment == null)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("Please submit a valid request"));
            }
            if (payment.Amount < 0)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("Please enter a valid Amount"));
            }

            if (!Enum.IsDefined(typeof(Currency), payment.Currency))
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("Please enter a valid Currency"));
            }

            if (payment.CardNumber.Length != 16 || !cardNumberRegex.IsMatch(payment.CardNumber))
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("Please enter a valid 16 digit Card Number"));
            }

            if (!Enumerable.Range(1, 12).Contains(payment.ExpiryMonth))
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("Please enter a valid 2 digit Card Expiry Month"));
            }

            if (!Enumerable.Range(0, 99).Contains(payment.ExpiryYear) || payment.ExpiryYear < (DateTime.Now.Year % 100))
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("Please enter a valid 2 digit Card Expiry Year"));
            }

            if (!Enumerable.Range(0, 999).Contains(payment.CVV))
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("Please enter a valid 2 digit Card CVV"));
            }

            payment.CardLast4Digits = Int32.Parse(payment.CardNumber.Substring(payment.CardNumber.Length - 4));
            payment.CurrencyId = (int)Enum.Parse(typeof(Currency), payment.Currency);

            var result = this.paymentService.CreatePaymentAsync(payment).Result;

            return this.Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// Gets an Payment by Id.
        /// </summary>
            // GET: /Payments/5
        [Route("{id}")]
        [HttpGet]
        public HttpResponseMessage GetPaymentById(Guid id)
        {
            try
            {
                var result = this.paymentService.GetPayment(id);

                if(!string.IsNullOrEmpty(result.UserMessage))
                {
                    return this.Request.CreateResponse(HttpStatusCode.NotFound, result.UserMessage);
                }

                return this.Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch(Exception ex)
            {
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        #endregion
    }
}