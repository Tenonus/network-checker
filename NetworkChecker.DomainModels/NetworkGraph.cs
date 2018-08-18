
using System.Collections.Generic;
using System.Linq;

namespace NetworkChecker.DomainModels
{
    public class NetworkGraph
    {
        public List<GraphNode> GraphNodes { get; set; }

        public void AddNode(Node node)
        {
            var graphNode = new GraphNode(node);
            GraphNodes = new List<GraphNode> {graphNode};
        }

        public void AddNode(string id, Node node)
        {
            var graphNode = new GraphNode(node);
            GraphNodes.FirstOrDefault(x => x.Id == id).GraphNodes.Add(graphNode);
        }
    }

    public class GraphNode
    {
        public GraphNode(Node node)
        {
            Id = node.Node1Id;
            GraphNodes = new List<GraphNode> {new GraphNode(node.Node2Id)};
        }

        public GraphNode(string id)
        {
            Id = id;
        }

        public string Id { get; set; }

        public List<GraphNode> GraphNodes { get; set; }
    }
}
