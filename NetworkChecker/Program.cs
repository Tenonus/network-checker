using System;
using NetworkChecker.BreakFinderStrategyAbstraction;
using NetworkChecker.CsvWorkerService;
using NetworkChecker.GraphBuilderAbstraction;
using NetworkChecker.NetworkBreakFinder;

namespace NetworkChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Введите названия [.csv] файла-хранилища для поиска разрывов{Environment.NewLine}" +
                              $"Либо, введите [q] для выхода");
            var value = Console.ReadLine();
            while (value != "q")
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Поиск разрывов в [{value}]");
                Console.ResetColor();

                try
                {
                    if (Search(value))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("В сети найдены разрывы");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("Разрывов не найдено");
                        Console.ResetColor();
                    }
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(e.Message);
                    Console.ResetColor();
                }

                value = Console.ReadLine();
            }
            
        }

        private static bool Search(string fileName)
        {
            IGraphBuilder builder = new CsvGraphBuilder();
            IBreakFinderStrategy strategy = new BreakFinderByWidthSearchStrategy();

            var monitor = new NetworkMonitor.NetworkMonitor(builder, strategy);
            return monitor.FindBreaksInNetwork(fileName);
        }
    }
}
