using System.Collections.Generic;

namespace NetworkChecker.DomainModels
{
    /// <summary>
    /// Сеть в виде графа
    /// </summary>
    public class Graph
    {
        public Graph(){}

        public Graph(IEnumerable<Node> nodes)
        {
            Nodes = nodes;
        }
        
        /// <summary>
        /// Список узлов графа
        /// </summary>
        public IEnumerable<Node> Nodes { get; set; }
    }
}
