using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace NetworkChecker.DomainModels.Tests
{
    [TestFixture]
    public class NetworkGraphTests
    {
        [Test]
        public void AddNode_Test()
        {
            var graph = new NetworkGraph();
            var nodes = new List<Node>
            {
                new Node {Node1Id = "1", Node2Id = "2"},
                new Node {Node1Id = "2", Node2Id = "3"},
                new Node {Node1Id = "3", Node2Id = "1"}
            };

            graph.AddNode(nodes.FirstOrDefault());
            nodes.Remove(nodes.FirstOrDefault());

            foreach (var node in nodes)
            {
                graph.AddNode(node.Node1Id, node);
            }

            var i = 10;
        }
    }
}
