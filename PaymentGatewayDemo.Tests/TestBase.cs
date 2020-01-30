/// ----------------------------------------------------------------------
/// <summary>
/// Defines the test base.
/// </summary>
/// ----------------------------------------------------------------------

namespace PaymentGatewayDemo.Tests
{
    using Moq;
    using NUnit.Framework;
    using PaymentGatewayDemo.APIClients.Interfaces;
    using PaymentGatewayDemo.Controllers;
    using PaymentGatewayDemo.Logging;
    using PaymentGatewayDemo.Repositories.Interfaces;
    using PaymentGatewayDemo.Services;
    using PaymentGatewayDemo.Services.Interfaces;
    using System.Net.Http;
    using System.Web.Http;

    /// <summary>
    /// The test base.
    /// </summary>
    [TestFixture]
    public abstract class TestBase
    {
        /// <summary>
        /// Gets or sets the Logger Mock.
        /// </summary>
        internal Mock<ILogger> LoggerMock { get; set; }

        /// <summary>
        /// Gets or sets the Payment Repository Mock.
        /// </summary>
        internal Mock<IPaymentRepository> PaymentRepositoryMock { get; set; }

        /// <summary>
        /// Gets or sets the Payment Service Mock.
        /// </summary>
        internal Mock<IPaymentService> PaymentServiceMock { get; set; }

        /// <summary>
        /// Gets or sets the Bank API Mock.
        /// </summary>
        internal Mock<IBankAPIClient> BankAPIMock { get; set; }

        /// <summary>
        ///Initializes the Payment controller.
        /// </summary>
        /// <returns><see cref="PaymentController"/></returns>
        internal PaymentController InitPaymentController()
        {
            var controller = new PaymentController(
                this.PaymentServiceMock.Object);

            controller.Request = new HttpRequestMessage();
            controller.Request.SetConfiguration(new HttpConfiguration());

            return controller;
        }

        /// <summary>
        ///Initializes the Payment service.
        /// </summary>
        /// <returns><see cref="PaymentController"/></returns>
        internal PaymentService InitPaymentService()
        {
            var service = new PaymentService(
                this.LoggerMock.Object,
                this.PaymentRepositoryMock.Object,
                this.BankAPIMock.Object);
            
            return service;
        }

        internal T GetContentFromResponse<T>(HttpResponseMessage response) where T : class
        {
            var objectcontent = response.Content as ObjectContent;

            if (objectcontent == null)
            {
                return default(T);
            }

            return objectcontent.Value as T;
        }
    }
}
