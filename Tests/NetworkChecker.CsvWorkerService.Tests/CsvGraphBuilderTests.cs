using System;
using System.IO;
using System.Linq;
using NetworkChecker.Configurations.Exceptions;
using NetworkChecker.DomainModels;
using NUnit.Framework;

namespace NetworkChecker.CsvWorkerService.Tests
{
    [TestFixture]
    public class CsvGraphBuilderTests
    {
        private CsvGraphBuilder _csv;

        [SetUp]
        public void SetUp()
        {
            _csv = new CsvGraphBuilder();
        }

        [Test]
        public void BuildGraph_WithCsvFileWhichHasThreeString_ShouldMapToThreeNodes()
        {
            // Arrange
            var fileName = "Nodes_3.csv";

            // Act
            var graph = _csv.BuildGraph(fileName);
            var count = graph.Nodes.Count();

            // Assert
            Assert.AreEqual(count, 3);
        }

        [Test]
        public void BuildGraph_WithNotExistingCsvFile_ShouldThrowsFileNotFoundException()
        {
            // Arrange
            var fileName = "Nodes_NotExists.csv";

            // Act
            Func<Graph> result = () => _csv.BuildGraph(fileName);

            // Assert
            Assert.Throws<FileNotFoundException>(() => result.Invoke());
        }

        [Test]
        public void BuildGraph_WithCsvFileWithoutHeaders_ShouldThrowsFileParsingException()
        {
            // Arrange
            var fileName = "Nodes_Without_Headers.csv";

            // Act
            Func<Graph> result = () => _csv.BuildGraph(fileName);

            // Assert
            Assert.Throws<FileParsingException>(() => result.Invoke());
        }

        [Test]
        public void BuildGraph_WithValidFileName_ShouldNotThrowsInvalidFileNameException(
            [Values("Nodes_3.csv", "Nodes_3.CSV", "Nodes_3.csv ")] string fileName)
        {
            // Act
            Func<Graph> result = () => _csv.BuildGraph(fileName);

            // Assert
            Assert.DoesNotThrow(() => result.Invoke());
        }

        [Test]
        public void BuildGraph_WithInvalidFileName_ShouldThrowsInvalidFileNameException(
            [Values(null, "", "asd", "Nodes_3.csq")] string fileName)
        {
            // Act
            Func<Graph> result = () => _csv.BuildGraph(fileName);

            // Assert
            Assert.Throws<InvalidFileNameException>(() => result.Invoke());
        }
        
        [Test]
        public void BuildGraph_WithNotValidCsvFile_ShouldThrowsCsvMapperException(
            [Values("Nodes_Not_Valid.csv", "Nodes_Except_FirstField.csv", "Nodes_Except_SecondField.csv")] string name)
        {
            // Arrange
            var fileName = name;

            // Act
            Func<Graph> result = () => _csv.BuildGraph(fileName);
            
            // Assert
            Assert.Throws<FileParsingException>(() => result.Invoke());
        }
    }
}
