/// ----------------------------------------------------------------------
/// <summary>
/// Defines the GetPayment tests.
/// </summary>
/// ----------------------------------------------------------------------

namespace PaymentGatewayDemo.Tests.Services.PaymentService
{
    using Moq;
    using NUnit.Framework;
    using PaymentGatewayDemo.Enums;
    using PaymentGatewayDemo.Models;
    using PaymentGatewayDemo.Models.ViewModels;
    using System;

    /// <summary>
    /// Get Payment Tests.
    /// </summary>
    [TestFixture]
    public class GetPaymentTests : TestBaseInitialiser
    {
        
        /// <summary>
        /// Should Return payment if exists
        /// </summary>
        [Test]
        public void ShouldReturnPaymentIfExists()
        {
            // Arrange
            var service = this.InitPaymentService();

            // Mock
            this.PaymentRepositoryMock.Setup(x => x.GetPaymentWithCardDetails(It.IsAny<Guid>())).Returns(new PaymentWithCardDetails
            {
                PaymentStatus = PaymentStatus.Complete,
                PaymentCode = Guid.Parse("E7C77306-9BDD-4987-9FBB-3B7C1C9C1A69"),
                CardDetails = new PaymentCardDetails()
            });

            // Act
            var result = service.GetPayment(Guid.NewGuid());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(PaymentStatus.Complete.ToString(), result.PaymentStatus);
            Assert.AreEqual(Guid.Parse("E7C77306-9BDD-4987-9FBB-3B7C1C9C1A69"), result.PaymentCode);
            Assert.IsNull(result.UserMessage);
        }

        /// <summary>
        /// Should Return payment if exists
        /// </summary>
        [Test]
        public void ShouldReturnUserMessageIfNoPaymentExists()
        {
            // Arrange
            var service = this.InitPaymentService();

            // Mock
            this.PaymentRepositoryMock.Setup(x => x.GetPaymentWithCardDetails(It.IsAny<Guid>())).Returns(
                (PaymentWithCardDetails)null);

            // Act
            var result = service.GetPayment(Guid.NewGuid());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(PaymentStatus.Failed.ToString(), result.PaymentStatus);
            Assert.AreEqual("No payment found matching this ID", result.UserMessage);
        }
    }
}
