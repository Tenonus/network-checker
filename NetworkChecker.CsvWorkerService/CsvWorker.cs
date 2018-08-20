using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using NetworkChecker.CsvWorkerService.Exceptions;
using NetworkChecker.CsvWorkerService.Infrastructure;
using NetworkChecker.DomainModels;

namespace NetworkChecker.CsvWorkerService
{
    /// <summary>
    /// Класс для парсинга .CSV файлов
    /// </summary>
    public class CsvWorker
    {
        /// <summary>
        /// Получить коллекцию узлов из файла
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <returns>Коллекция узлов</returns>
        public Graph GetNodes(string fileName)
        {
            var name = !string.IsNullOrWhiteSpace(fileName)
                ? fileName
                : throw new ArgumentNullException(nameof(fileName));
            var path = Path.Combine(Environment.CurrentDirectory, name);
            try
            {
                var nodes = ParseCsv(path);
                return new Graph(nodes);
            }
            catch (CsvHelper.MissingFieldException e)
            {
                throw new CsvMapperException("Invalid csv file structure. Field is missing", e);
            }
        }

        /// <summary>
        /// Парсинг .CSV файла
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <returns>Коллекция узлов</returns>
        private IEnumerable<Node> ParseCsv(string path)
        {
            using (TextReader reader = File.OpenText(path))
            {
                var csv = new CsvReader(reader);
                csv.Configuration.Delimiter = ";";
                csv.Configuration.RegisterClassMap<NodeMapperProfile>();
                var result = csv.GetRecords<Node>().ToList();
                if (!ValidateNodes(result)) throw new CsvMapperException();
                return result;
            }
        }

        /// <summary>
        /// Проверка на то, что заполнены обе вершины узла
        /// </summary>
        /// <param name="nodes">Коллекция узлов</param>
        private bool ValidateNodes(IEnumerable<Node> nodes)
        {
            return nodes.All(a => !string.IsNullOrWhiteSpace(a.Node1Id)) &&
                   nodes.All(a => !string.IsNullOrWhiteSpace(a.Node2Id));

        }
    }
}
