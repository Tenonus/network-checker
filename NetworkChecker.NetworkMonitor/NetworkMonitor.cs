using System;
using NetworkChecker.BreakFinderStrategyAbstraction;
using NetworkChecker.GraphBuilderAbstraction;

namespace NetworkChecker.NetworkMonitor
{
    /// <summary>
    /// Монитор разрывов в сети
    /// </summary>
    public class NetworkMonitor
    {
        /// <summary>
        /// Строитель графа сети
        /// </summary>
        private readonly IGraphBuilder _graphBuilder;

        /// <summary>
        /// Стратегия поиска разрывов
        /// </summary>
        private readonly IBreakFinderStrategy _breakFinderStrategy;

        public NetworkMonitor(IGraphBuilder graphBuilder, IBreakFinderStrategy breakFinderStrategy)
        {
            _graphBuilder = graphBuilder ?? throw new ArgumentNullException(nameof(graphBuilder));
            _breakFinderStrategy = breakFinderStrategy ?? throw new ArgumentNullException(nameof(breakFinderStrategy));
        }

        /// <summary>
        /// Поиск разрыва в сети
        /// </summary>
        /// <param name="fileName">Имя файла-хранилища сигнатуры сети</param>
        /// <returns>Признак наличия разрыва</returns>
        public bool FindBreaksInNetwork(string fileName)
        {
            var graph = _graphBuilder.BuildGraph(fileName);
            var isGraphHasBreak = _breakFinderStrategy.FindBreaks(graph);

            return isGraphHasBreak;
        }
    }
}
