using System;
using System.Net;

using AcmeInsurance.Claims.Models;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Unity;

namespace AcmeInsurance.Claims.WebServices.Proxy.Tests
{
    [TestClass]
    public class ClaimsProxyClaimTests
    {
        [TestMethod]
        public void AddClaim_ProviderIsNull_ThrowsException()
        {
            // Arrange
            ClaimsProxy proxy = new ClaimsProxy();
            IClaimModel claim = UnityConfig.Container.Resolve<IClaimModel>();
            claim.PatientName = "John Doe";
            claim.Provider = null;
            claim.Amount = 0.0M;
            claim.HasPreApproval = false;

            // Act
            // Assert
            Assert.ThrowsException<WebException>(() => proxy.AddClaim(claim));
        }

        [TestMethod]
        public void AddClaimAsync_ProviderIsNull_ThrowsException()
        {
            // Arrange
            ClaimsProxy proxy = new ClaimsProxy();
            IClaimModel claim = UnityConfig.Container.Resolve<IClaimModel>();
            claim.PatientName = "John Doe";
            claim.Provider = null;
            claim.Amount = 0.0M;
            claim.HasPreApproval = false;

            // Act
            // Assert
            Assert.ThrowsException<AggregateException>(() => proxy.AddClaimAsync(claim).Wait());
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        public void GetClaimStatus_ClaimDoesNotExist_ThrowsException(int id)
        {
            // Arrange
            ClaimsProxy proxy = new ClaimsProxy();

            // Act
            // Assert
            Assert.ThrowsException<WebException>(() => proxy.GetClaimStatus(id));
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        public void GetClaimStatusAsync_ClaimDoesNotExist_ThrowsException(int id)
        {
            // Arrange
            ClaimsProxy proxy = new ClaimsProxy();

            // Act
            // Assert
            Assert.ThrowsException<AggregateException>(
                () => proxy.GetClaimStatusAsync(id).Wait()
            );
        }
    }
}
