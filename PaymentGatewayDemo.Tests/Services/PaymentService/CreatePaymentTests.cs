/// ----------------------------------------------------------------------
/// <summary>
/// Defines the CreatePayment tests.
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
    using System.Net;

    /// <summary>
    /// Add Payment Tests.
    /// </summary>
    [TestFixture]
    public class CreatePaymentTests : TestBaseInitialiser
    {
        /// <summary>
        /// Should Return Error if Payment Insert fails.
        /// </summary>
        [Test]
        public void ShouldReturnErrorIfPaymentInsertFails()
        {
            // Arrange
            var service = this.InitPaymentService();

            // Mock
            this.PaymentRepositoryMock.Setup(x => x.Add<Payment, int>(It.IsAny<Payment>())).Returns((Payment)null);
            this.LoggerMock.Setup(x => x.LogFatal(It.IsAny<Exception>(), It.IsAny<string>()));

            // Act
            var result = service.CreatePaymentAsync(new PaymentCardDetailsInputViewModel()).Result;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(PaymentStatus.Error.ToString(), result.PaymentStatus);
        }

        /// <summary>
        /// Should Return Error if Card Details Insert fails.
        /// </summary>
        [Test]
        public void ShouldReturnErrorIfCardDetailsInsertFails()
        {
            // Arrange
            var service = this.InitPaymentService();

            // Mock
            this.PaymentRepositoryMock.Setup(x => x.Add<Payment, int>(It.IsAny<Payment>())).Returns(new Payment());
            this.PaymentRepositoryMock.Setup(x => x.Add<PaymentCardDetails, int>(It.IsAny<PaymentCardDetails>())).Returns((PaymentCardDetails)null);
            this.LoggerMock.Setup(x => x.LogFatal(It.IsAny<Exception>(), It.IsAny<string>()));

            // Act
            var result = service.CreatePaymentAsync(new PaymentCardDetailsInputViewModel()).Result;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(PaymentStatus.Error.ToString(), result.PaymentStatus);
        }

        /// <summary>
        /// Should Return Failed if Bank authorization fails.
        /// </summary>
        [Test]
        public void ShouldReturnFailedIfBankAuthorizationFails()
        {
            // Arrange
            var service = this.InitPaymentService();

            // Mock
            this.PaymentRepositoryMock.Setup(x => x.Add<Payment, int>(It.IsAny<Payment>())).Returns(new Payment());
            this.PaymentRepositoryMock.Setup(x => x.Update<Payment, int>(It.IsAny<Payment>(), It.IsAny<Func<Payment, int>>())).Returns(new Payment
            {
                PaymentCode = Guid.Parse("E7C77306-9BDD-4987-9FBB-3B7C1C9C1A69"),
                PaymentStatusId = (int)PaymentStatus.Failed
            });
            this.PaymentRepositoryMock.Setup(x => x.Add<PaymentCardDetails, int>(It.IsAny<PaymentCardDetails>())).Returns(new PaymentCardDetails());
            this.BankAPIMock.Setup(x => x.InitiatePaymentAuthorizationAsync(It.IsAny<BankAPIRequest>())).ReturnsAsync(new BankAPIResponse
            {
                Success = false
            });
            this.LoggerMock.Setup(x => x.LogError(It.IsAny<string>()));

            // Act
            var result = service.CreatePaymentAsync(new PaymentCardDetailsInputViewModel()).Result;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(PaymentStatus.Failed.ToString(), result.PaymentStatus);
        }

        /// <summary>
        /// Should Return Complete if Bank authorization.
        /// </summary>
        [Test]
        public void ShouldReturnCompleteIfBankAuthorization()
        {
            // Arrange
            var service = this.InitPaymentService();

            // Mock
            this.PaymentRepositoryMock.Setup(x => x.Add<Payment, int>(It.IsAny<Payment>())).Returns(new Payment {
               PaymentCode = Guid.Parse("E7C77306-9BDD-4987-9FBB-3B7C1C9C1A69")
            });
            this.PaymentRepositoryMock.Setup(x => x.Update<Payment, int>(It.IsAny<Payment>(), It.IsAny<Func<Payment, int>>())).Returns(new Payment {
                PaymentCode = Guid.Parse("E7C77306-9BDD-4987-9FBB-3B7C1C9C1A69"),
                PaymentStatusId = (int)PaymentStatus.Complete
            });
            this.PaymentRepositoryMock.Setup(x => x.Add<PaymentCardDetails, int>(It.IsAny<PaymentCardDetails>())).Returns(new PaymentCardDetails());
            this.BankAPIMock.Setup(x => x.InitiatePaymentAuthorizationAsync(It.IsAny<BankAPIRequest>())).ReturnsAsync(new BankAPIResponse
            {
                Success = true
            });
            // Act
            var result = service.CreatePaymentAsync(new PaymentCardDetailsInputViewModel()).Result;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(PaymentStatus.Complete.ToString(), result.PaymentStatus);
            Assert.AreEqual(Guid.Parse("E7C77306-9BDD-4987-9FBB-3B7C1C9C1A69"), result.PaymentCode);
        }
    }
}
