using NetworkChecker.DomainModels;

namespace NetworkChecker.BreakFinderStrategyAbstraction
{
    public interface IBreakFinderStrategy
    {
        bool FindBreaks(Graph graph);
    }
}