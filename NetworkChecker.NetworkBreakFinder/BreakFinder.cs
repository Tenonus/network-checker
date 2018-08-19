using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkChecker.DomainModels;

namespace NetworkChecker.NetworkBreakFinder
{
    public class BreakFinder
    {
        private List<string> _stack = new List<string>();
        private List<string> _usedNodes = new List<string>();

        public bool FindBreaks(IEnumerable<Node> nodes)
        {
            var node = nodes.FirstOrDefault().Node1Id;
            while (node != null)
            {
                node = GetNextStep(node, nodes);
                if (node != null)
                {
                    _usedNodes.Add(node);
                    _stack.Add(node);
                }
                else
                {
                    node = nodes.FirstOrDefault(x => x.Node1Id == _stack.LastOrDefault())?.Node1Id;
                    if (!string.IsNullOrWhiteSpace(node)) _stack.Remove(node);
                }
            }

            var un = GetUniqueNodeIds(nodes);
            var list = un.Except(_usedNodes);
            var isAgain = true;
            while (isAgain)
            {
                var r = nodes.Where(x => list.Contains(x.Node1Id) || list.Contains(x.Node2Id));
                var qwe = r.Where(a => _usedNodes.Contains(a.Node1Id)).Select(s => s.Node2Id);
                var qwe1 = r.Where(a => _usedNodes.Contains(a.Node2Id)).Select(s => s.Node1Id);
                _usedNodes.AddRange(qwe);
                _usedNodes.AddRange(qwe1);
                isAgain = qwe.Any() || qwe1.Any();
            }
            list = un.Except(_usedNodes);
            var res = list.Any();
            return res;
        }

        private IEnumerable<string> GetUniqueNodeIds(IEnumerable<Node> nodes)
        {
            var allNodes = nodes.Select(s => s.Node1Id).Union(nodes.Select(s => s.Node2Id));
            return allNodes.Distinct();
        }

        private string GetNextStep(string id, IEnumerable<Node> nodes)
        {
            var relativeNodes = nodes.Where(x => x.Node1Id == id && !_usedNodes.Contains(x.Node2Id));
            var result = relativeNodes.FirstOrDefault();
            return result?.Node2Id;
        }

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
