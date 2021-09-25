using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StarLog.Services;

namespace StarLog.ApiTests.Services
{
    [TestClass]
    public class ObserverationServiceTests
    {
        private ObservationService _service;

        [TestInitialize]
        public void Initialize()
        {
            _service = new ObservationService();
        }

        [TestMethod]
        public async Task GetObservationsForUserAsync_should_return_observations_for_user_when_available()
        {
            // Arrange
            // TODO: arrange test data via dependencies
            
            // Act
            var result = await _service.GetObservationsForUserAsync(TestData.TestUserId);

            // Assert
            Assert.IsTrue(result.Count > 0);
        }

        private static class TestData
        {
            public static string TestUserId = "7573f1d69c40c45120a91cf675ec34d6";
        }
    }
}