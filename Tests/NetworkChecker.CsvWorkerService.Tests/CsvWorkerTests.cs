using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NetworkChecker.CsvWorkerService.Exceptions;
using NetworkChecker.DomainModels;
using NUnit.Framework;

namespace NetworkChecker.CsvWorkerService.Tests
{
    [TestFixture]
    public class CsvWorkerTests
    {
        private CsvWorker _csv;

        [SetUp]
        public void SetUp()
        {
            _csv = new CsvWorker();
        }

        [Test]
        public void GetNodes_WithCsvFileWhichHasThreeString_ShouldMapToThreeNodes()
        {
            // Arrange
            var fileName = "Nodes_3.csv";

            // Act
            var nodes = _csv.GetNodes(fileName);
            var count = nodes.Count();

            // Assert
            Assert.AreEqual(count, 3);
        }

        [Test]
        public void GetNodes_WithNotExistingCsvFile_ShouldThrowsFileNotFoundException()
        {
            // Arrange
            var fileName = "Nodes_NotExists.csv";

            // Act
            Func<IEnumerable<Node>> result = () => _csv.GetNodes(fileName);

            // Assert
            Assert.Throws<FileNotFoundException>(() => result.Invoke());
        }

        [Test]
        public void GetNodes_WithNotValidCsvFile_ShouldThrowsCsvMapperException(
            [Values("Nodes_Not_Valid.csv", "Nodes_Except_FirstField.csv", "Nodes_Except_SecondField.csv")] string name)
        {
            // Arrange
            var fileName = name;

            // Act
            Func<IEnumerable<Node>> result = () => _csv.GetNodes(fileName);
            
            // Assert
            Assert.Throws<CsvMapperException>(() => result.Invoke());
        }
    }
}
