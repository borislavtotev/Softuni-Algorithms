using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_FractionalKnapsackProblem
{
    class FractionalKnapsackProblem
    {
        static Item[] allItems;

        static void Main(string[] args)
        {
            var capacityParams = Console.ReadLine().Split(' ');
            var capacity = int.Parse(capacityParams[1]);

            var itemsParams = Console.ReadLine().Split(' ');
            var items = int.Parse(itemsParams[1]);
            allItems = new Item[items]; 

            for (int i = 0; i < items; i++)
            {
                var newItemParams = Console.ReadLine().Split(' ');
                allItems[i] = new Item(int.Parse(newItemParams[2]), int.Parse(newItemParams[0]));
            }

            var currentWeight = 0;
            var index = 0;
            var sortedItems = allItems.OrderByDescending(i => i).ToList();
            var result = new Dictionary<Item, double>();
            double totalPrice = 0;

            while (currentWeight < capacity && index < items)
            {
                var currentItem = sortedItems[index];
                var openCapacity = capacity - currentWeight;
                if (openCapacity >= currentItem.Weight)
                {
                    result.Add(currentItem, 100);
                    currentWeight += currentItem.Weight;
                    totalPrice += currentItem.Price;
                }
                else
                {
                    double percent = (double)openCapacity / (double)currentItem.Weight;
                    result.Add(currentItem, Math.Round(percent, 4) * 100);
                    currentWeight = capacity;
                    totalPrice += currentItem.Price * percent;
                }

                index++;
            }

            foreach (var item in result)
            {
                Console.WriteLine("Take {0:0.00}% of item with price {1:0.00} and weight {2:0.00}", item.Value, item.Key.Price, item.Key.Weight);
            }

            Console.WriteLine("Total price: {0:0.00}", totalPrice);
        }
    }

    public class Item : IComparable<Item>
    {
        public Item(int weight, int price)
        {
            this.Weight = weight;
            this.Price = price;
        }

        public int Price { get; private set; }

        public int Weight { get; private set; }

        public int CompareTo(Item other)
        {
            double currentPricePerUnit = this.Price / this.Weight;
            double otherPricePerUnit = other.Price / other.Weight;

            return currentPricePerUnit.CompareTo(otherPricePerUnit);
        }
    }
}
