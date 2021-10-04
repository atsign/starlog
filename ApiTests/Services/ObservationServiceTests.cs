using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Azure.Cosmos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StarLog.Data;
using StarLog.Entities;
using StarLog.Models;
using StarLog.Services;

namespace StarLog.ApiTests.Services
{
    [TestClass]
    public class ObserverationServiceTests
    {
        private Mock<ICosmosDbRepositoryFactory> _mockFactory;
        private Mock<ICosmosDbRepository> _mockRepository;
        private Mock<IMapper> _mockMapper;
        private ObservationService _service;

        [TestInitialize]
        public void Initialize()
        {
            _mockFactory = new Mock<ICosmosDbRepositoryFactory>();
            _mockRepository = new Mock<ICosmosDbRepository>();
            _mockMapper = new Mock<IMapper>();

            _mockMapper.Setup(mapper =>
                mapper.Map<List<ObservationModel>>(It.IsAny<List<Observation>>()))
                .Returns<List<Observation>>(observations => FakeMap(observations));

            _mockFactory.Setup(factory =>
                factory.GetCosmosDbRepository(Constants.ContainerNames.Observations))
                .Returns(_mockRepository.Object);

            _service = new ObservationService(
                _mockFactory.Object,
                _mockMapper.Object);
        }

        private List<ObservationModel> FakeMap(List<Observation> observations) {
            return observations
                .Select(observation => new ObservationModel { Id = observation.Id })
                .ToList();
        }

        [TestMethod]
        public void Constructor_should_create_Observations_repository_from_factory_when_called()
        {
            // Assert
            _mockFactory.Verify(factory =>
                factory.GetCosmosDbRepository(Constants.ContainerNames.Observations), Times.Exactly(1));
        }

        [TestMethod]
        public async Task GetObservationsForUserAsync_should_return_observations_for_user_when_available()
        {
            // Arrange
            _mockRepository.Setup(repo =>
                repo.GetItemsAsync<Observation>(It.IsAny<QueryDefinition>()))
                .ReturnsAsync(TestData.TestObservations);

            // Act
            var result = await _service.GetObservationsForUserAsync(TestData.TestUserId);

            // Assert
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public async Task GetObservationsForUserAsync_should_return_empty_list_when_user_has_no_observations()
        {
            // Act
            var result = await _service.GetObservationsForUserAsync(TestData.TestUserId);

            // Assert
            Assert.IsTrue(result.Count == 0);
        }

        private static class TestData
        {
            public static string TestUserId = "7573f1d69c40c45120a91cf675ec34d6";
            public static List<Observation> TestObservations = new List<Observation>
            {
                new Observation
                {
                    Id = "12345"
                }
            };
        }
    }
}