using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkChecker.DomainModels;

namespace NetworkChecker.NetworkBreakFinder
{
    public class BreakFinder
    {
        public bool FindBreakByNodeId(IEnumerable<Node> nodes)
        {
            var idsEnumerable = GetIdsEnumerable(nodes);
            foreach (var node in nodes)
            {
                var segments = nodes
                    .Where(x => x.Node1NmbId == node.Node1NmbId 
                                || x.Node2NmbId == node.Node1NmbId);

                var isInNetwork = segments.Any(a => 
                    idsEnumerable.Count(c => c == a.Node1NmbId) >= 2 
                    || idsEnumerable.Count(c => c == a.Node2NmbId) >= 2);

                if (!isInNetwork) return true;
            }
            return false;
        }
        
        private IEnumerable<int> GetIdsEnumerable(IEnumerable<Node> nodes)
        {
            var result = new List<int>();
            foreach (var node in nodes)
            {
                if (node.Node1NmbId != node.Node2NmbId) result.AddRange(new[] {node.Node1NmbId, node.Node2NmbId});
                else result.Add(node.Node1NmbId);
            }

            return result;
        }
    }
}
