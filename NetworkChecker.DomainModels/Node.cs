namespace NetworkChecker.DomainModels
{
    /// <summary>
    /// Узел графа
    /// </summary>
    public class Node
    {
        /// <summary>
        /// Вершина-ключ
        /// </summary>
        public string Node1Id { get; set; }

        /// <summary>
        /// Вершина-значение
        /// </summary>
        public string Node2Id { get; set; }
    }
}
