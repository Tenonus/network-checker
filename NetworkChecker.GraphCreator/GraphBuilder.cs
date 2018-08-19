using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkChecker.DomainModels;
using NetworkChecker.GraphCreator.Extensions;

namespace NetworkChecker.GraphCreator
{
    public class GraphBuilder
    {
        public bool GenerateGraph(IEnumerable<Node> simpleNodes)
        {
            var uniqieNodeIds = GetUniqueNodeIds(simpleNodes);
            var dict = FillDict(uniqieNodeIds);
            var linkedNodes = uniqieNodeIds.ToLinkedNodes(simpleNodes);
            SetRelatedNodes(linkedNodes);
            var res = FindBreaks(linkedNodes, dict);
            return res;
        }

        public bool FindBreaks(IEnumerable<LinkedNode> nodes, Dictionary<string, int> dict)
        {
            // Открываем цикл по всем узлам
            foreach (var linkedNode in nodes)
            {
                    // Получаем все неиспользованные связанные узлы текущего узла
                var nmn = GetNonMarkedRelatedNodes(linkedNode, dict);
                // Идём по всем связанным узлам
                foreach (var nextNode in nmn)
                {
                    // Берём узел
                    // Помечаем его как использованный
                    dict[nextNode.Id] = 1;
                    // Повторяем операцию 
                    FindBreaks(nextNode.AllNodes, dict);
                }
            }

            var res = dict.All(a => a.Value != 0);
            return res;
        }

        private void SetRelatedNodes(IEnumerable<LinkedNode> nodes)
        {
            foreach (var linkedNode in nodes)
            {
                foreach (var linkedNodeAllNode in linkedNode.AllNodes)
                {
                    linkedNodeAllNode.AllNodes = GetRelatedNodes(linkedNode.Id, nodes);
                }
            }
        }

        private IEnumerable<LinkedNode> GetRelatedNodes(string id, IEnumerable<LinkedNode> nodes)
        {
            return nodes.Where(x => x.AllNodes.Select(s => s.Id).Contains(id));
        }

        private IEnumerable<LinkedNode> GetNonMarkedRelatedNodes(LinkedNode node, Dictionary<string, int> dict)
        {
            var nodeIds = node.AllNodes.Select(s => s.Id).Distinct();
            var nonMarkedDictElems = dict.Where(x => x.Value == 0).Select(s => s.Key);
            var nonMarkedNodeIds = nodeIds.Intersect(nonMarkedDictElems);

            return node.AllNodes.Where(x => nonMarkedNodeIds.Contains(x.Id));
        }

        private IEnumerable<string> GetUniqueNodeIds(IEnumerable<Node> nodes)
        {
            var allNodes = nodes.Select(s => s.Node1Id);
            return allNodes.Distinct();
        }

        private Dictionary<string, int> FillDict(IEnumerable<string> ids)
        {
            return ids.ToDictionary(item => item, item => 0);
        }
    }
}
