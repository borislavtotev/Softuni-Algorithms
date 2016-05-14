using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;


namespace _05_Election
{
    class Program
    {
        static void Main(string[] args)
        {
            int k = int.Parse(Console.ReadLine());
            int n = int.Parse(Console.ReadLine());

            BigInteger[] parties = new BigInteger[n];
            for (int i = 0; i < n; i++)
            {
                parties[i] = BigInteger.Parse(Console.ReadLine());
            }

            var possibleCombinations = CalcPossibleSums(parties);
            var sunsHigherThanK = possibleCombinations.Where(c => c.Key >= k).ToList();
            BigInteger result = 0;
            foreach (var sum in sunsHigherThanK)
            {
                result += sum.Value;
            }
            Console.WriteLine(result);
        }

        static IDictionary<BigInteger, BigInteger> CalcPossibleSums(BigInteger[] nums)
        {
            var possibleSums = new Dictionary<BigInteger, BigInteger>();
            possibleSums.Add(0, 1);
            for (int i = 0; i < nums.Length; i++)
            {
                var newSums = new Dictionary<BigInteger, BigInteger>();
                var oldSumsForIncrease = new Dictionary<BigInteger, BigInteger>();
                foreach (var sum in possibleSums.Keys)
                {
                    BigInteger newSum = sum + nums[i];
                    if (!possibleSums.ContainsKey(newSum))
                    {
                        if (!newSums.ContainsKey(newSum))
                        {
                            newSums.Add(newSum, 0);
                        }

                        newSums[newSum] += possibleSums[sum];
                    }
                    else
                    {
                        if (!oldSumsForIncrease.ContainsKey(newSum))
                        {
                            oldSumsForIncrease.Add(newSum, 0);
                        }
                        oldSumsForIncrease[newSum] += possibleSums[sum];
                    }
                }

                foreach (var sum in newSums)
                {
                    possibleSums.Add(sum.Key, sum.Value);
                }

                foreach (var sum in oldSumsForIncrease)
                {
                    possibleSums[sum.Key] += sum.Value;
                }
            }
            return possibleSums;
        }
    }
}
