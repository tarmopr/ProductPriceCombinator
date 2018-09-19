using System;

namespace ProductPriceCombinator
{
    public class ProductListGenerator
    {
        private readonly Random _random = new Random();

        public int[] Generate(decimal targetPrice, decimal[] itemPrices, int minItemCount, out decimal totalPrice)
        {
            totalPrice = 0;
            int itemTotalCount = 0;
            int[] items = new int[itemPrices.Length];
            
            while (totalPrice < targetPrice)
            {
                int nextItem = _random.Next(items.Length);
                decimal price = itemPrices[nextItem];
                
                if (totalPrice + price > targetPrice)
                {
                    price = FindSmallestPrice(targetPrice, items, totalPrice, ref nextItem);
                }
                else if (totalPrice + price < targetPrice && itemTotalCount + 1 > minItemCount)
                {
                    price = FindTargetPrice(targetPrice, items, totalPrice, ref nextItem);
                }

                items[nextItem]++;
                totalPrice += price;
                itemTotalCount++;
            }

            return items;
        }

        private static decimal FindTargetPrice(decimal targetPrice, int[] items, decimal totalPrice, ref int nextItem)
        {
            int index = -1; decimal price = 0;
            for (int i = 0; i < items.Length; i++)
            {
                if (i == nextItem) 
                    continue;
                
                int newPrice = items[i];
                if (index >= 0 && newPrice <= price) 
                    continue;
                
                index = i;
                price = newPrice;

                if (totalPrice + newPrice == targetPrice)
                    break;
            }
            nextItem = index;
            return price;
        }

        private static decimal FindSmallestPrice(decimal targetPrice, int[] items, decimal totalPrice, ref int nextItem)
        {
            int index = -1; decimal price = 0;
            for (int i = 0; i < items.Length; i++)
            {
                if (i == nextItem) 
                    continue;
                
                int newPrice = items[i];
                if (index >= 0 && newPrice >= price) 
                    continue;
                
                index = i;
                price = newPrice;

                if (totalPrice + newPrice == targetPrice)
                    break;
            }
            nextItem = index;
            return price;
        }
    }
}
