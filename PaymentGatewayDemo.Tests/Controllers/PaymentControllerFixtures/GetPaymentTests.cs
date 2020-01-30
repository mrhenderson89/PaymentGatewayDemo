/// ----------------------------------------------------------------------
/// <summary>
/// Defines the GetPayment tests.
/// </summary>
/// ----------------------------------------------------------------------

namespace PaymentGatewayDemo.Tests.Controllers.PaymentControllerFixtures
{
    using Moq;
    using NUnit.Framework;
    using PaymentGatewayDemo.Enums;
    using PaymentGatewayDemo.Models.ViewModels;
    using System;
    using System.Net;

    /// <summary>
    /// Get Payment Tests.
    /// </summary>
    [TestFixture]
    public class GetPaymentTests : TestBaseInitialiser
    {
        /// <summary>
        /// Should Return NotFound if no payment.
        /// </summary>
        [Test]
        public void ShouldReturnNotFoundIfNoPayment()
        {
            // Arrange
            var controller = this.InitPaymentController();

            // Mock
            this.PaymentServiceMock.Setup(x => x.GetPayment(It.IsAny<Guid>())).Returns(new PaymentResultViewModel {
                UserMessage = "Error"
            });

            // Act
            var result = controller.GetPaymentById(Guid.NewGuid());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }

        /// <summary>
        /// Should Return OK if payment found.
        /// </summary>
        [Test]
        public void ShouldReturnOkIfPaymentFound()
        {
            // Arrange
            var controller = this.InitPaymentController();

            // Mock
            this.PaymentServiceMock.Setup(x => x.GetPayment(It.IsAny<Guid>())).Returns(new PaymentResultViewModel
            {
                PaymentCode = Guid.NewGuid(),
                PaymentStatus = PaymentStatus.Complete.ToString()
            });

            // Act
            var result = controller.GetPaymentById(Guid.NewGuid());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
