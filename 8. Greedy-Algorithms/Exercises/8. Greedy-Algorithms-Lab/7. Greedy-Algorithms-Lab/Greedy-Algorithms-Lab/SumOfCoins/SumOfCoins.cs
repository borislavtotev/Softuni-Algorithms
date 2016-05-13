namespace SumOfCoins
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SumOfCoins
    {
        public static void Main(string[] args)
        {
            var availableCoins = new[] { 1, 2, 5, 10, 20, 50 };
            var targetSum = 923;

            var selectedCoins = ChooseCoins(availableCoins, targetSum);

            Console.WriteLine($"Number of coins to take: {selectedCoins.Values.Sum()}");
            foreach (var selectedCoin in selectedCoins)
            {
                Console.WriteLine($"{selectedCoin.Value} coin(s) with value {selectedCoin.Key}");
            }
        }

        public static Dictionary<int, int> ChooseCoins(IList<int> coins, int targetSum)
        {
            var result = new Dictionary<int, int>();
            var sortedCoins = coins.OrderByDescending(c => c).ToList();

            var sum = 0;
            var index = 0;
            while (sum < targetSum && index < coins.Count)
            {
                var currentCoin = sortedCoins[index];
                var remainingSum = targetSum - sum;
                var numberOfCoinsToTake = remainingSum / currentCoin;
                if (numberOfCoinsToTake > 0)
                {
                    if (!result.ContainsKey(currentCoin))
                    {
                        result[currentCoin] = 0;
                    }

                    result[currentCoin] = numberOfCoinsToTake;
                    sum += currentCoin * numberOfCoinsToTake;
                }

                index++;
            }

            if (sum != targetSum)
            {
                throw new InvalidOperationException("Greedy algorithm cannot produce desired sum with specified coins.");
            }

            return result;
        }
    }
}