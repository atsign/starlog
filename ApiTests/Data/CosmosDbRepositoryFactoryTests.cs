using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StarLog.Data;
using StarLog.Entities;
using Microsoft.Azure.Cosmos;
using System.Collections.Generic;
using System;

namespace StarLog.ApiTests.Data
{
    [TestClass]
    public class CosmosDbRepositoryFactoryTests
    {
        private CosmosDbRepositoryFactory _factory;

        [TestInitialize]
        public void Initialize()
        {
            _factory = new CosmosDbRepositoryFactory(
                TestData.TestDbName, TestData.TestContainerNames, new Mock<CosmosClient>().Object);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void GetCosmosDbRepository_should_throw_ArgumentException_when_containerName_is_not_in_collection()
        {
            // Arrange
            var testContainerName = "NonexistantContainer";

            // Act
            var repository = _factory.GetCosmosDbRepository(testContainerName);
        }

        [TestMethod]
        public void GetCosmosDbRepository_should_return_CosmosDbRepository_instance_when_containerName_exists()
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
            public static List<string> TestContainerNames = new List<string> { "TestContainer1", "TestContainer2" };
        }
    }
}
