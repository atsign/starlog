using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StarLog.Data;
using Microsoft.Azure.Cosmos;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.Options;
using StarLog.Options;

namespace StarLog.ApiTests.Data
{
    [TestClass]
    public class CosmosDbRepositoryFactoryTests
    {
        private CosmosDbRepositoryFactory _factory;

        [TestInitialize]
        public void Initialize()
        {
            var mockCosmosOptions = new Mock<IOptions<CosmosDbOptions>>();
            
            mockCosmosOptions.SetupGet(opts => opts.Value)
                .Returns(new CosmosDbOptions
                {
                    DatabaseName = TestData.TestDbName
                });

            _factory = new CosmosDbRepositoryFactory(mockCosmosOptions.Object, new Mock<CosmosClient>().Object);
        }

        [TestMethod]
        public void GetCosmosDbRepository_should_return_CosmosDbRepository_instance_when_called()
        {
            // Arrange
            var testContainerName = "TestContainer1";

            // Act
            var repository = _factory.GetCosmosDbRepository(testContainerName);

            // Assert
            Assert.IsNotNull(repository);
        }

        private static class TestData
        {
            public static string TestDbName = "TestDb";
        }
    }
}
