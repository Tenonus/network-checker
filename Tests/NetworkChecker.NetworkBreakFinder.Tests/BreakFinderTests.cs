using System.Collections.Generic;
using NetworkChecker.DomainModels;
using NUnit.Framework;

namespace NetworkChecker.NetworkBreakFinder.Tests
{
    [TestFixture]
    public class BreakFinderTests
    {
        private List<Node> _nodesWhichHaventBreaks;
        private BreakFinder _breakFinder;

        [SetUp]
        public void SetUp()
        {
            _breakFinder = new BreakFinder();

            _nodesWhichHaventBreaks = new List<Node>
            {
                new Node {Node1NmbId = 1, Node1Id = "Node1", Node2NmbId = 2, Node2Id = "Node2"},
                new Node {Node1NmbId = 2, Node1Id = "Node2", Node2NmbId = 3, Node2Id = "Node3"},
                new Node {Node1NmbId = 3, Node1Id = "Node3", Node2NmbId = 1, Node2Id = "Node1"}
            };
        }

        [Test]
        public void FindBreaks_WithListOfNodesWhichHaventBreaks_ShouldReturnFalse()
        {
            // Act
            var result = _breakFinder.FindBreaks(_nodesWhichHaventBreaks);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void FindBreaks_WithListOfNodesWhichHaveOneBreak_ShouldReturnTrue()
        {
            // Arrange
            var nodesList = _nodesWhichHaventBreaks;
            nodesList.Add(new Node { Node1NmbId = 5, Node1Id = "Node5", Node2NmbId = 4, Node2Id = "Node4" });

            // Act
            var result = _breakFinder.FindBreaks(nodesList);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void FindBreaks_WithListOfNodesWhichHaveOneKnottyBreak_ShouldReturnTrue()
        {
            // Arrange
            var nodesList = _nodesWhichHaventBreaks;
            nodesList.AddRange(new[]
            {
                new Node {Node1NmbId = 5, Node1Id = "Node5", Node2NmbId = 4, Node2Id = "Node4"},
                new Node {Node1NmbId = 4, Node1Id = "Node4", Node2NmbId = 5, Node2Id = "Node5"}
            });

            // Act
            var result = _breakFinder.FindBreaks(nodesList);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void GenerateGraph1()
        {
            // Arrange
            var nodes = new List<Node>();
            nodes.AddRange(new[]
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

            // Act
            var result = _breakFinder.FindBreaks(nodes);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void GenerateGraph_WithCircularArc()
        {
            // Arrange
            var nodes = new List<Node>();
            nodes.AddRange(new[]
            {
                new Node{Node1Id = "N2", Node2Id = "N3"},
                new Node{Node1Id = "N1", Node2Id = "N2"},
                new Node{Node1Id = "N3", Node2Id = "N1"},
                new Node{Node1Id = "N1", Node2Id = "N1"},
                new Node{Node1Id = "N2", Node2Id = "N2"},
                new Node{Node1Id = "N5", Node2Id = "N4"},
                new Node{Node1Id = "N4", Node2Id = "N2"}
            });

            // Act
            var result = _breakFinder.FindBreaks(nodes);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void GenerateGraph2_WithCircularArc()
        {
            // Arrange
            var nodes = new List<Node>();
            nodes.AddRange(new[]
            {
                new Node{Node1Id = "N2", Node2Id = "N3"},
                new Node{Node1Id = "N1", Node2Id = "N2"},
                new Node{Node1Id = "N3", Node2Id = "N1"},
                new Node{Node1Id = "N1", Node2Id = "N1"},
                new Node{Node1Id = "N2", Node2Id = "N2"},
                new Node{Node1Id = "N5", Node2Id = "N4"},
                new Node{Node1Id = "N4", Node2Id = "N2"},
                new Node{Node1Id = "N6", Node2Id = "N7"},
                new Node{Node1Id = "N7", Node2Id = "N6"},
                new Node{Node1Id = "N6", Node2Id = "N6"},
            });

            // Act
            var result = _breakFinder.FindBreaks(nodes);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void FindBreakByNodeId_WithListOfNodesWhichHaventBreaks_ShouldReturnFalse()
        {
            // Act
            var result = _breakFinder.FindBreakByNodeId(_nodesWhichHaventBreaks);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void FindBreakByNodeId_WithListOfNodesWhichHaveOneNonDirectionalNode_ShouldReturnFalse()
        {
            // Arrange
            var nodesList = _nodesWhichHaventBreaks;
            nodesList.Add(new Node {Node1NmbId = 4, Node1Id = "Node4", Node2NmbId = 1, Node2Id = "Node1"});

            // Act
            var result = _breakFinder.FindBreakByNodeId(nodesList);

            // Assert
            Assert.IsFalse(result);
        }


        [Test]
        public void FindBreakByNodeId_WithKnottyListOfNodesWhichHaventBreaks_ShouldReturnFalse()
        {
            // Arrange
            var nodesList = _nodesWhichHaventBreaks;
            nodesList.AddRange(new []
            {
                new Node { Node1NmbId = 2, Node1Id = "Node2", Node2NmbId = 2, Node2Id = "Node2" },
                new Node { Node1NmbId = 3, Node1Id = "Node3", Node2NmbId = 2, Node2Id = "Node2" },
                new Node { Node1NmbId = 4, Node1Id = "Node4", Node2NmbId = 1, Node2Id = "Node1" }
            });

            // Act
            var result = _breakFinder.FindBreakByNodeId(nodesList);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void FindBreakByNodeId_WithListOfNodesWhichHaveOneBreak_ShouldReturnTrue()
        {
            // Arrange
            var nodesList = _nodesWhichHaventBreaks;
            nodesList.Add(new Node {Node1NmbId = 5, Node1Id = "Node5", Node2NmbId = 4, Node2Id = "Node4"});

            // Act
            var result = _breakFinder.FindBreakByNodeId(nodesList);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void FindBreakByNodeId_WithListOfNodesWhichHaveOneKnottyBreak_ShouldReturnTrue()
        {
            // Arrange
            var nodesList = _nodesWhichHaventBreaks;
            nodesList.AddRange(new[]
            {
                new Node {Node1NmbId = 5, Node1Id = "Node5", Node2NmbId = 4, Node2Id = "Node4"},
                new Node {Node1NmbId = 4, Node1Id = "Node4", Node2NmbId = 5, Node2Id = "Node5"}
            });

            // Act
            var result = _breakFinder.FindBreakByNodeId(nodesList);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void FindBreakByNodeId_WithListOfNodesWhichHaveBreakInCircularArc_ShouldReturnTrue()
        {
            // Arrange
            var nodesList = _nodesWhichHaventBreaks;
            nodesList.Add(new Node {Node1NmbId = 4, Node1Id = "Node4", Node2NmbId = 4, Node2Id = "Node4"});

            // Act
            var result = _breakFinder.FindBreakByNodeId(nodesList);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
