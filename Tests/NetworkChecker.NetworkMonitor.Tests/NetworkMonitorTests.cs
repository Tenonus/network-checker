using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using NetworkChecker.BreakFinderStrategyAbstraction;
using NetworkChecker.Configurations.Exceptions;
using NetworkChecker.DomainModels;
using NetworkChecker.GraphBuilderAbstraction;
using NUnit.Framework;

namespace NetworkChecker.NetworkMonitor.Tests
{
    [TestFixture]
    public class NetworkMonitorTests
    {
        private readonly Mock<IGraphBuilder> _builder = new Mock<IGraphBuilder>();
        private readonly Mock<IBreakFinderStrategy> _strategy = new Mock<IBreakFinderStrategy>();

        [SetUp]
        public void SetUp()
        {
            _builder.Setup(s => s.BuildGraph(It.IsAny<string>())).Returns(new Graph());
            _strategy.Setup(s => s.FindBreaks(It.IsAny<Graph>())).Returns(false);
        }

        [Test]
        public void FindBreaksInNetwork_WithUnBreakedGraphInFile_ShouldReturnFalse()
        {
            // Arrange
            var networkMonitor = new NetworkMonitor(_builder.Object, _strategy.Object);
            var fileName = "somefile.csv";

            // Act
            var result = networkMonitor.FindBreaksInNetwork(fileName: fileName);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
