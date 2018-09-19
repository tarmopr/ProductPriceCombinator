using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ProductPriceCombinator
{
    public class CombinationFinder
    {
        private readonly ProductListGenerator _generator;
        private readonly string[] _itemNames;
        private readonly decimal[] _itemPrices;
        private readonly HashSet<int> _resultHashes = new HashSet<int>();

        public decimal TargetPrice { get; set; }
        public int MinProductCount { get; set; }
        public bool OutputIterationBestResult { get; set; }
        
        public int MaxItemCount { get; private set; }
        public double AverageItemCount { get; private set; }
        public int MinTotalItemCount { get; private set; }
        public int[] BestResult { get; private set; }

        public CombinationFinder(IDictionary<string, decimal> items, decimal targetPrice, int minProductCount, bool outputIterationBestResult = true)
        {
            _generator = new ProductListGenerator();
            
            _itemNames = new List<string>(items.Keys).ToArray();
            _itemPrices = new List<decimal>(items.Values).ToArray();
            
            TargetPrice = targetPrice;
            MinProductCount = minProductCount;
            OutputIterationBestResult = outputIterationBestResult;
        }

        private bool ValidateInput()
        {
            if (MinProductCount > _itemPrices.Length)
            {
                Console.WriteLine($"Min product count {MinProductCount} is larger than product count {_itemPrices.Length}");
                return false;
            }

            var ordered = _itemPrices.OrderBy(i => i).ToArray();
            decimal totalPrice = 0;
            for (int i = 0; i < MinProductCount; i++)
            {
                totalPrice += ordered[i];
            }

            if (totalPrice > TargetPrice)
            {
                Console.WriteLine($"Target price {TargetPrice} is smaller than min product count price sum {totalPrice}");
                return false;
            }

            return true;
        }

        public void FindBestCombination(TimeSpan maxTime)
        {
            if (!ValidateInput())
                return;

            var sw = new Stopwatch();
            sw.Start();

            MaxItemCount = 0;
            MinTotalItemCount = 0;
            BestResult = null;

            while(sw.ElapsedMilliseconds < maxTime.TotalMilliseconds)
            {
                var items = _generator.Generate(TargetPrice, _itemPrices, MinProductCount, out decimal totalPrice);
                if (totalPrice != TargetPrice || items.Count(i => i > 0) < MinProductCount)
                    continue;
                
                if (SetIfCurrentBestResult(items) && OutputIterationBestResult)
                    OutputCurrentBestResult();
            }
            
            sw.Stop();
        }

        private bool SetIfCurrentBestResult(int[] items)
        {
            int maxIndividualItemCount = 0;
            int itemTotalCount = 0;
            int moreThanZeroItemCount = 0;

            foreach (int itemCount in items)
            {
                if (maxIndividualItemCount < itemCount)
                    maxIndividualItemCount = itemCount;
                itemTotalCount += itemCount;
                if (itemCount > 0) moreThanZeroItemCount++;
            }

            double averageItemCount = itemTotalCount / (double) moreThanZeroItemCount;
            if (BestResult != null && (itemTotalCount > MinTotalItemCount || maxIndividualItemCount > MaxItemCount || averageItemCount > AverageItemCount))
                return false;

            int hash = GetHash(items);
            if (_resultHashes.Contains(hash))
                return false;

            _resultHashes.Add(hash);

            BestResult = items;
            MinTotalItemCount = itemTotalCount;
            MaxItemCount = maxIndividualItemCount;
            AverageItemCount = averageItemCount;
            
            return true;
        }

        public IDictionary<string, int> GetBestProductCounts()
        {
            if (BestResult == null)
                return null;

            var dict = new Dictionary<string, int>();
            for (int i = 0; i < BestResult.Length; i++)
            {
                dict.Add(_itemNames[i], BestResult[i]);
            }

            return dict;
        }

        public void OutputCurrentBestResult()
        {
            if (BestResult == null)
            {
                Console.WriteLine("No best result set. Run FindBestCombination() first.");
                return;
            }

            var sb = new StringBuilder();
            for (int i = 0; i < BestResult.Length; i++)
            {
                if (i > 0) sb.Append(", ");
                sb.Append($"'{_itemNames[i]}'x{BestResult[i]}");
            }

            Console.WriteLine($"{MinTotalItemCount} ({MaxItemCount}/{AverageItemCount:F1}): {sb}");
        }

        private static int GetHash(int[] array)
        {
            int hc = array.Length;
            foreach (int item in array)
            {
                hc = unchecked(hc*17 +item);
            }
            return hc;
        }
    }
}
