using CsvHelper.Configuration;
using NetworkChecker.DomainModels;

namespace NetworkChecker.CsvWorkerService.Infrastructure
{
    public sealed class NodeMapperProfile : ClassMap<Node>
    {
        public NodeMapperProfile()
        {
            Map(m => m.Node1Id);
            Map(m => m.Node2Id);
        }
    }
}
