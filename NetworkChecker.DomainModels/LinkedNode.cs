using System.Collections.Generic;

namespace NetworkChecker.DomainModels
{
    public class LinkedNode
    {
        public LinkedNode(string id)
        {
            Id = id;
        }

        public LinkedNode()
        {
            
        }

        public string Id { get; set; }

        public IEnumerable<LinkedNode> PrevNodes { get; set; }

        public IEnumerable<LinkedNode> NextNodes { get; set; }

        public IEnumerable<LinkedNode> AllNodes { get; set; }
    }
}
