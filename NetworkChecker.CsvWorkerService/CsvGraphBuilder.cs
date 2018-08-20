using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using NetworkChecker.Configurations.Exceptions;
using NetworkChecker.CsvWorkerService.Infrastructure;
using NetworkChecker.DomainModels;
using NetworkChecker.GraphBuilderAbstraction;

namespace NetworkChecker.CsvWorkerService
{
    /// <summary>
    /// Класс для парсинга .CSV файлов
    /// </summary>
    public class CsvGraphBuilder : IGraphBuilder
    {
        private const string FILE_PARSING_ERROR_MESSAGE =
            "Ошибка построения графа сети. Возможно, одна из вершин узла не задана";

        /// <summary>
        /// Получить коллекцию узлов из файла
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <returns>Коллекция узлов</returns>
        public Graph BuildGraph(string fileName)
        {
            if (!ValidateFileName(fileName))
                throw new InvalidFileNameException("Имя файла не задано или имеет неверное расширение");
            var path = Path.Combine(Environment.CurrentDirectory, fileName);
            try
            {
                var nodes = ParseCsv(path);
                return new Graph(nodes);
            }
            catch (CsvHelper.MissingFieldException e)
            {
                throw new FileParsingException(FILE_PARSING_ERROR_MESSAGE, e);
            }
        }

        /// <summary>
        /// Проверка названия файла на пустоту и неправильное расширение
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <returns>Признак валидности</returns>
        private bool ValidateFileName(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) return false;
            var extenstion = Path.GetExtension(fileName.TrimEnd());
            return extenstion.ToLower() == ".csv";
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
                var result = new List<Node>();
                try
                {
                    result = csv.GetRecords<Node>().ToList();
                }
                catch (ValidationException)
                {
                    throw new FileParsingException("Ошибка построения графа сети. Возможно, заголовки узла в источнике не заданы или заданы неправильно");
                }
                if (!ValidateNodes(result)) throw new FileParsingException(FILE_PARSING_ERROR_MESSAGE);
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
