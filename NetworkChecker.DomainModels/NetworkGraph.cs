
namespace NetworkChecker.DomainModels
{
    public class NetworkGraph
    {
        public GraphNode[] GraphNodes { get; set; }

        public void AddNode(GraphNode node)
        {

        }
    }

    public class GraphNode
    {
        public string Id { get; set; }

        public GraphNode[] GraphNodes { get; set; }
    }
}
