using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkChecker.DomainModels;

namespace NetworkChecker.GraphCreator.Extensions
{
    public static class GraphBuilderExtensions
    {
        public static LinkedNode ToLinkedNode(this string id)
        {
            return new LinkedNode(id);
        }

        public static IEnumerable<LinkedNode> ToLinkedNodes(this IEnumerable<string> nodeIds)
        {
            return nodeIds.Select(nodeId => nodeId.ToLinkedNode()).ToList();
        }

        public static IEnumerable<LinkedNode> ToLinkedNodes(this IEnumerable<string> uniqieNodeIds,
            IEnumerable<Node> simpleNodes)
        {
            var result = new List<LinkedNode>();
            foreach (var uniqieNodeId in uniqieNodeIds)
            {
                var node = new LinkedNode();
                node.Id = uniqieNodeId;
                node.NextNodes = simpleNodes.Where(x => x.Node1Id == node.Id).Select(s => s.Node2Id).ToLinkedNodes();
                node.PrevNodes = simpleNodes.Where(x => x.Node2Id == node.Id).Select(s => s.Node1Id).ToLinkedNodes();
                node.AllNodes = node.NextNodes.Union(node.PrevNodes);
                result.Add(node);
            }

            return result;
        }
    }
}
