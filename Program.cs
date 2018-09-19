using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ProductPriceCombinator
{
    class Program
    {
        private const int MinProductCount = 6;
        private const decimal TargetPrice = 10.0m;
        
        private static readonly TimeSpan MaxCalculationTime = TimeSpan.FromSeconds(5);
        private static readonly Dictionary<string, decimal> ProductPrices = new Dictionary<string, decimal>
        {
            { "Estrella krõpsud", 1.53m },
            { "Kõrremahl", 0.32m },
            { "Pulgakomm", 0.13m },
            { "Twixi batoon", 0.38m },
            { "Geisha shokolaad", 1.15m },
            { "Väikese Tomi jäätis", 0.51m },
            { "Kellukese limonaad", 0.77m },
            { "Orbiti näts", 0.45m },
        };

        static void Main(string[] args)
        {
            var finder = new CombinationFinder(ProductPrices, TargetPrice, MinProductCount);

            Console.WriteLine("Start");

            var sw = new Stopwatch();
            sw.Start();
            finder.FindBestCombination(MaxCalculationTime);
            sw.Stop();

            Console.WriteLine($"Best result:");

            finder.OutputCurrentBestResult();

            Console.WriteLine($"End ({sw.Elapsed})");

            Console.Read();
        }
    }
}
