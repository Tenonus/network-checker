using System;
using System.Collections.Generic;
using NetworkChecker.DomainModels;
using NUnit.Framework;

namespace NetworkChecker.NetworkBreakFinder.Tests
{
    [TestFixture]
    public class NetworkBreakFinderTests
    {
        private List<Node> _nodesWhichHaventBreaks;
        private NetworkBreakFinder _breakFinder;

        [SetUp]
        public void SetUp()
        {
            _breakFinder = new NetworkBreakFinder();
            _nodesWhichHaventBreaks = new List<Node>
            {
                new Node {Node1Id = "Node1", Node2Id = "Node2"},
                new Node {Node1Id = "Node2", Node2Id = "Node3"},
                new Node {Node1Id = "Node3", Node2Id = "Node1"}
            };
        }

        [Test]
        public void FindBreaks_InGraphWithoutBreaks_ShouldReturnFalse()
        {
            // Arrange
            var graph = new Graph(_nodesWhichHaventBreaks);

            // Act
            var result = _breakFinder.FindBreaks(graph);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void FindBreaks_InGraphWithOneNonDirectionalNode_ShouldReturnFalse()
        {
            // Arrange
            var nodesList = _nodesWhichHaventBreaks;
            nodesList.Add(new Node {Node1Id = "Node4", Node2Id = "Node1"});
            var graph = new Graph(nodesList);

            // Act
            var result = _breakFinder.FindBreaks(graph);

            // Assert
            Assert.IsFalse(result);
        }
        
        [Test]
        public void FindBreaks_InGraphWithKnottyStructureWithoutBreaks_ShouldReturnFalse()
        {
            // Arrange
            var nodesList = _nodesWhichHaventBreaks;
            nodesList.AddRange(new[]
            {
                new Node { Node1Id = "Node2", Node2Id = "Node2" },
                new Node { Node1Id = "Node3", Node2Id = "Node2" },
                new Node { Node1Id = "Node4", Node2Id = "Node1" }
            });
            var graph = new Graph(nodesList);

            // Act
            var result = _breakFinder.FindBreaks(graph);

            // Assert
            Assert.IsFalse(result);
        }
        
        [Test]
        public void FindBreaks_InGraphWithCircularArc_ShouldReturn_False()
        {
            // Arrange
            var nodesList = new List<Node>();
            nodesList.AddRange(new[]
            {
                new Node{Node1Id = "N2", Node2Id = "N3"},
                new Node{Node1Id = "N1", Node2Id = "N2"},
                new Node{Node1Id = "N3", Node2Id = "N1"},
                new Node{Node1Id = "N1", Node2Id = "N1"},
                new Node{Node1Id = "N2", Node2Id = "N2"},
                new Node{Node1Id = "N5", Node2Id = "N4"},
                new Node{Node1Id = "N4", Node2Id = "N2"}
            });
            var graph = new Graph(nodesList);

            // Act
            var result = _breakFinder.FindBreaks(graph);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void FindBreaks_InGraphWithOneBreak_ShouldReturnTrue()
        {
            // Arrange
            var nodesList = _nodesWhichHaventBreaks;
            nodesList.Add(new Node { Node1Id = "Node5", Node2Id = "Node4" });
            var graph = new Graph(nodesList);

            // Act
            var result = _breakFinder.FindBreaks(graph);

            // Assert
            Assert.IsTrue(result);
        }
        
        [Test]
        public void FindBreaks_InGraphWithBreakInCircularArc_ShouldReturnTrue()
        {
            // Arrange
            var nodesList = _nodesWhichHaventBreaks;
            nodesList.Add(new Node { Node1Id = "Node4", Node2Id = "Node4" });
            var graph = new Graph(nodesList);

            // Act
            var result = _breakFinder.FindBreaks(graph);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void FindBreaks_InGraphWithKnottyStructureBreak_ShouldReturnTrue()
        {
            // Arrange
            var nodesList = new List<Node>();
            nodesList.AddRange(new[]
            {
                new Node{Node1Id = "N2", Node2Id = "N3"},
                new Node{Node1Id = "N1", Node2Id = "N2"},
                new Node{Node1Id = "N3", Node2Id = "N1"},
                new Node{Node1Id = "N1", Node2Id = "N1"},
                new Node{Node1Id = "N5", Node2Id = "N4"},
                new Node{Node1Id = "N4", Node2Id = "N2"},
                new Node{Node1Id = "N6", Node2Id = "N7"},
                new Node{Node1Id = "N7", Node2Id = "N6"},
                new Node{Node1Id = "N6", Node2Id = "N6"},
            });
            var graph = new Graph(nodesList);

            // Act
            var result = _breakFinder.FindBreaks(graph);

            // Assert
            Assert.IsTrue(result);
        }
        
        [Test]
        public void FindBreaks_InNullableGraphStructure_ShouldThrowsArgumentNullException()
        {
            // Arrange
            IEnumerable<Node> nodesList = null;
            var graph = new Graph(nodesList);

            // Act
            Func<bool> result = () => _breakFinder.FindBreaks(graph);

            // Assert
            Assert.Throws<ArgumentNullException>(() => result.Invoke());
        }

        [Test]
        public void FindBreaks_InGraphWithEmptyListOfNodes_ShouldThrowsArgumentNullException()
        {
            // Arrange
            IEnumerable<Node> nodesList = new List<Node>();
            var graph = new Graph(nodesList);

            // Act
            Func<bool> result = () => _breakFinder.FindBreaks(graph);

            // Assert
            Assert.Throws<ArgumentNullException>(() => result.Invoke());
        }
    }
}
