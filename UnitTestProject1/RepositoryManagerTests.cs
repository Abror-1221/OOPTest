using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using YourProject;

namespace UnitTestProject1
{
    [TestClass]
    public class RepositoryManagerTests
    {
        [TestMethod]
        public void Initialize_ShouldCreateEmptyRepository()
        {
            // Arrange & Act
            RepositoryManager manager = new RepositoryManager();

            // Assert
            Assert.IsNotNull(manager); // Ensure manager is created
        }

        [TestMethod]
        public void Register_ValidJsonForType1_ShouldSucceed()
        {
            // Arrange
            RepositoryManager manager = new RepositoryManager();
            string validJson = "{\"name\": \"John Doe\", \"age\": 30}";

            // Act
            manager.Register("item1", validJson, 1);

            // Assert
            string retrievedContent = manager.Retrieve("item1");
            Assert.AreEqual(validJson, retrievedContent);
        }

        [TestMethod]
        public void Register_ValidXmlForType2_ShouldSucceed()
        {
            // Arrange
            RepositoryManager manager = new RepositoryManager();
            string validXml = "<person><name>John Doe</name><age>30</age></person>";

            // Act
            manager.Register("item1", validXml, 2);

            // Assert
            string retrievedContent = manager.Retrieve("item1");
            Assert.AreEqual(validXml, retrievedContent);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Register_InvalidJsonForType1_ShouldThrowException()
        {
            // Arrange
            RepositoryManager manager = new RepositoryManager();
            string invalidJson = "invalid json";

            // Act
            manager.Register("item1", invalidJson, 1);

            // Assert
            // The ExpectedException attribute ensures that an exception is thrown
        }

        [TestMethod]
        public void Retrieve_ExistingItem_ShouldReturnContent()
        {
            // Arrange
            RepositoryManager manager = new RepositoryManager();
            string content = "{\"name\": \"Bambang\", \"age\": 10}";
            manager.Register("item1", content, 1);

            // Act
            string retrievedContent = manager.Retrieve("item1");

            // Assert
            Assert.AreEqual(content, retrievedContent);

        }

        [TestMethod]
        public void Retrieve_NonExistentItem_ShouldReturnNull()
        {
            // Arrange
            RepositoryManager manager = new RepositoryManager();

            // Act
            string retrievedContent = manager.Retrieve("nonexistentItem");

            // Assert
            Assert.IsNull(retrievedContent);
        }

        [TestMethod]
        public void GetType_ExistingItem_ShouldReturnItemType()
        {
            // Arrange
            RepositoryManager manager = new RepositoryManager();
            string content = "{\"name\": \"Bambang\", \"age\": 10}";
            manager.Register("item1", content, 1);

            // Act
            int itemType = manager.GetType("item1");

            // Assert
            Assert.AreEqual(1, itemType);
        }

        [TestMethod]
        public void GetType_NonExistentItem_ShouldReturnNegativeOne()
        {
            // Arrange
            RepositoryManager manager = new RepositoryManager();

            // Act
            int itemType = manager.GetType("nonexistentItem");

            // Assert
            Assert.AreEqual(-1, itemType);
        }

        [TestMethod]
        public void Deregister_ExistingItem_ShouldRemoveItem()
        {
            // Arrange
            RepositoryManager manager = new RepositoryManager();
            string content = "{\"name\": \"Bambang\", \"age\": 10}";
            manager.Register("item1", content, 1);

            // Act
            manager.Deregister("item1");

            // Assert
            Assert.IsNull(manager.Retrieve("item1"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Deregister_NonExistentItem_ShouldThrowException()
        {
            // Arrange
            RepositoryManager manager = new RepositoryManager();

            // Act
            manager.Deregister("nonexistentItem");

            // Assert
            // The ExpectedException attribute ensures that an ArgumentException is thrown
        }

    }
}
