using Microsoft.VisualStudio.TestTools.UnitTesting;
using StarLog.Data;
using StarLog.Entities;
using Moq;
using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace StarLog.ApiTests.Data
{
    [TestClass]
    public class CosmosDbRepositoryTests
    {
        private CosmosDbRepository _repository;
        private Mock<CosmosClient> _mockClient;
        private Mock<Container> _mockContainer;

        [TestInitialize]
        public void Initialize()
        {
            _mockClient = new Mock<CosmosClient>();
            _mockContainer = new Mock<Container>();

            _mockClient.Setup(client =>
                client.GetContainer(TestData.TestDatabase, TestData.TestContainer))
                .Returns(_mockContainer.Object);

            _repository = new CosmosDbRepository(
                _mockClient.Object, TestData.TestDatabase, TestData.TestContainer);
        }

        [TestMethod]
        public async Task AddItemAsync_should_call_container_CreateItemAsync_with_given_entity()
        {
            // Arrange
            var item = new TestEntity();

            // Act
            await _repository.AddItemAsync(item);

            // Assert
            _mockContainer.Verify(container =>
                container.CreateItemAsync(item, 
                                          null,
                                          It.IsAny<ItemRequestOptions>(),
                                          It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task DeleteItemAsync_should_call_container_DeleteItemAsync_with_given_entity()
        {
            // Arrange
            var item = new TestEntity { Id = "12345" };

            // Act
            await _repository.DeleteItemAsync(item);

            // Assert
            _mockContainer.Verify(container =>
                container.DeleteItemAsync<TestEntity>(item.Id, 
                                                      It.IsAny<PartitionKey>(),
                                                      It.IsAny<ItemRequestOptions>(),
                                                      It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task UpdateItemAsync_should_call_container_UpsertItemAsync_with_given_entity_id_and_entity()
        {
            // Arrange
            var item = new TestEntity { Id = "12345" };

            // Act
            await _repository.UpdateItemAsync(item.Id, item);

            // Assert
            _mockContainer.Verify(container =>
                container.UpsertItemAsync<TestEntity>(item,
                                                      It.IsAny<PartitionKey>(),
                                                      It.IsAny<ItemRequestOptions>(),
                                                      It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task GetItemAsync_should_return_null_when_container_ReadItemAsync_throws_CosmosException_with_NotFound_status()
        {
            // Arrange
            var itemId = "12345";
            _mockContainer.Setup(container => container.ReadItemAsync<TestEntity>(itemId,
                                                                      It.IsAny<PartitionKey>(),
                                                                      It.IsAny<ItemRequestOptions>(),
                                                                      It.IsAny<CancellationToken>()))
                .Throws(new CosmosException("Test error", System.Net.HttpStatusCode.NotFound, 1, string.Empty, 0));

            // Act
            var result = await _repository.GetItemAsync<TestEntity>(itemId);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(CosmosException))]
        public async Task GetItemAsync_should_throw_CosmosException_when_container_ReadItemAsync_throws_CosmosException_without_NotFound_status()
        {
            // Arrange
            var itemId = "12345";
            _mockContainer.Setup(container => container.ReadItemAsync<TestEntity>(itemId,
                                                                      It.IsAny<PartitionKey>(),
                                                                      It.IsAny<ItemRequestOptions>(),
                                                                      It.IsAny<CancellationToken>()))
                .Throws(new CosmosException("Test error", System.Net.HttpStatusCode.InternalServerError, 1, string.Empty, 0));

            // Act
            var result = await _repository.GetItemAsync<TestEntity>(itemId);
        }

        [TestMethod]
        public async Task GetItemAsync_should_return_Resource_from_container_ItemResponse_when_item_exists()
        {
            // Arrange
            var itemId = "12345";
            var testResponseMock = new Mock<ItemResponse<TestEntity>>();
            var expectedResponse = new TestEntity();

            testResponseMock.SetupGet(response => response.Resource)
                .Returns(expectedResponse);
            _mockContainer.Setup(container => container.ReadItemAsync<TestEntity>(itemId,
                                                                      It.IsAny<PartitionKey>(),
                                                                      It.IsAny<ItemRequestOptions>(),
                                                                      It.IsAny<CancellationToken>()))
                .ReturnsAsync(testResponseMock.Object);

            // Act
            var result = await _repository.GetItemAsync<TestEntity>(itemId);

            // Assert
            Assert.AreEqual(result, expectedResponse);
        }

        [TestMethod]
        public async Task GetItemsAsync_should_return_an_empty_List_when_there_are_no_results()
        {
            // Arrange
            string testQuery = "This is a test query";
            Mock<FeedIterator<TestEntity>> mockFeedIterator = new Mock<FeedIterator<TestEntity>>();
            Mock<FeedResponse<TestEntity>> mockFeedResponse = new Mock<FeedResponse<TestEntity>>();

            _mockContainer.Setup(container =>
                container.GetItemQueryIterator<TestEntity>(It.IsAny<QueryDefinition>(), null, null))
                .Returns(mockFeedIterator.Object);
            
            mockFeedIterator.SetupGet(iterator => iterator.HasMoreResults)
                .Returns(false);

            // Act
            var result = await _repository.GetItemsAsync<TestEntity>(testQuery);

            // Assert
            Assert.AreEqual(0, result.Count());
        }

        private static class TestData
        {
            public static string TestDatabase = "TestDatabase";
            public static string TestContainer = "TestContainer";
        }

        public class TestEntity : Entity { }
    }
}
