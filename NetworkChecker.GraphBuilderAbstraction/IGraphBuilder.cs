using NetworkChecker.DomainModels;

namespace NetworkChecker.GraphBuilderAbstraction
{
    public interface IGraphBuilder
    {
        Graph BuildGraph(string fileName);
    }
}