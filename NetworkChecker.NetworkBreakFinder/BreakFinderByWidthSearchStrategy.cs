using System;
using System.Collections.Generic;
using System.Linq;
using NetworkChecker.BreakFinderStrategyAbstraction;
using NetworkChecker.DomainModels;

namespace NetworkChecker.NetworkBreakFinder
{
    /// <summary>
    /// Объект для поиска разрывов сети
    /// </summary>
    public class BreakFinderByWidthSearchStrategy : IBreakFinderStrategy
    {
        /// <summary>
        /// Стэк проходов по графу
        /// </summary>
        private readonly List<string> _stack = new List<string>();
        
        /// <summary>
        /// Использованные вершины графа
        /// </summary>
        private readonly List<string> _usedNodes = new List<string>();

        /// <summary>
        /// Поиск разрыва в сети
        /// </summary>
        /// <param name="graph">Представление сети в виде графа</param>
        /// <returns>Признак существования разрыва</returns>
        public bool FindBreaks(Graph graph)
        {
            if (graph == null) throw new ArgumentNullException(nameof(graph));
            if (!graph.Nodes.Any()) throw new ArgumentNullException(nameof(graph.Nodes));
            var node = graph.Nodes.FirstOrDefault().Node1Id;
            while (node != null)
            {
                node = GetNextStep(node, graph.Nodes);
                if (node != null)
                {
                    _usedNodes.Add(node);
                    _stack.Add(node);
                }
                else
                {
                    node = graph.Nodes.FirstOrDefault(x => x.Node1Id == _stack.LastOrDefault())?.Node1Id;
                    if (!string.IsNullOrWhiteSpace(node)) _stack.Remove(node);
                }
            }

            var isBreakHasFound = CheckLostNodesForNetworkConnectivity(graph.Nodes);
            return isBreakHasFound;
        }

        /// <summary>
        /// Вернуть следующую вершину графа
        /// </summary>
        /// <param name="id">Идентификатор текущей вершины</param>
        /// <param name="nodes">Список узлов графа</param>
        /// <returns>Идентификатор следующей вершины графа</returns>
        private string GetNextStep(string id, IEnumerable<Node> nodes)
        {
            var relativeNodes = nodes.Where(x => x.Node1Id == id && !_usedNodes.Contains(x.Node2Id));
            var result = relativeNodes.FirstOrDefault();
            return result?.Node2Id;
        }

        /// <summary>
        /// Проверка потеряных узлов на соединение к сети
        /// </summary>
        /// <param name="nodes">Список узлов графа</param>
        /// <returns>Признак того, что потерянные узлы соединены в сеть</returns>
        private bool CheckLostNodesForNetworkConnectivity(IEnumerable<Node> nodes)
        {
            var uniqueNodeIds = GetUniqueNodeIds(nodes);
            var unusedNodeIds = uniqueNodeIds.Except(_usedNodes);
            var isAgain = true;
            while (isAgain)
            {
                var unusedNodes = nodes.Where(x => unusedNodeIds.Contains(x.Node1Id) || unusedNodeIds.Contains(x.Node2Id));
                var nodesWithConnectivityByKey = unusedNodes.Where(a => _usedNodes.Contains(a.Node2Id)).Select(s => s.Node1Id);
                var nodesWithConnectivityByValue = unusedNodes.Where(a => _usedNodes.Contains(a.Node1Id)).Select(s => s.Node2Id);

                _usedNodes.AddRange(nodesWithConnectivityByKey);
                _usedNodes.AddRange(nodesWithConnectivityByValue);

                isAgain = nodesWithConnectivityByValue.Any() || nodesWithConnectivityByKey.Any();
            }
            var lostNodes = uniqueNodeIds.Except(_usedNodes);

            return lostNodes.Any();
        }

        /// <summary>
        /// Получить список уникальных идентификаторов вершин
        /// </summary>
        /// <param name="nodes">Список узлов графа</param>
        /// <returns>Список вершин</returns>
        private IEnumerable<string> GetUniqueNodeIds(IEnumerable<Node> nodes)
        {
            var allNodes = nodes.Select(s => s.Node1Id).Union(nodes.Select(s => s.Node2Id));
            return allNodes.Distinct();
        }
    }
}
