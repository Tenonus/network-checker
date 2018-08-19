using System;
using System.Collections.Generic;
using System.Text;
using NetworkChecker.DomainModels;
using NUnit.Framework;

namespace NetworkChecker.GraphCreator.Tests
{
    [TestFixture]
    public class GraphBuilderTests
    {
        [Test]
        public void GenerateGraph()
        {
            var builder = new GraphBuilder();
            var nodes = new List<Node>();
            nodes.AddRange(new []
            {
                new Node{Node1Id = "N2", Node2Id = "N3"}, 
                new Node{Node1Id = "N1", Node2Id = "N2"}, 
                new Node{Node1Id = "N3", Node2Id = "N1"}, 
                new Node{Node1Id = "N1", Node2Id = "N1"}, 
                new Node{Node1Id = "N5", Node2Id = "N4"}, 
                new Node{Node1Id = "N4", Node2Id = "N2"}
            });
            var result = builder.GenerateGraph(nodes);

            Assert.IsTrue(result);
        }

        [Test]
        public void GenerateGraph1()
        {
            var builder = new GraphBuilder();
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
            var result = builder.GenerateGraph(nodes);

            Assert.IsFalse(result);
        }
    }
}
