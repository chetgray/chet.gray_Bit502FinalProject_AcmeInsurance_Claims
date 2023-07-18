using AcmeInsurance.Claims.Business.Tests.TestDoubles;
using AcmeInsurance.Claims.Data;
using AcmeInsurance.Claims.Data.Objects;
using AcmeInsurance.Claims.Models;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Unity;

namespace AcmeInsurance.Claims.Business.Tests
{
    [TestClass]
    public class ClaimBlTests
    {
        [TestMethod]
        [DataRow(ClaimStatus.Pending)]
        [DataRow(ClaimStatus.Approved)]
        [DataRow(ClaimStatus.Denied)]
        public void GetClaimStatus_ClaimExists_ReturnsClaimStatus(ClaimStatus expected)
        {
            // Arrange
            const int id = 1;
            UnityConfig.Container.RegisterInstance<IClaimRepository>(
                new ClaimRepositoryStub
                {
                    ClaimDto = new ClaimDto { Id = id, ClaimStatusId = (int)expected }
                }
            );
            IClaimBl bl = UnityConfig.Container.Resolve<IClaimBl>();

            // Act
            ClaimStatus actual = bl.GetClaimStatus(id);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
