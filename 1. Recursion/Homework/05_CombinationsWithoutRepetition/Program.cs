using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05_CombinationsWithoutRepetition
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int k = int.Parse(Console.ReadLine());
            GetCombinations(new int[k], n, k, 1, 0);
        }

        static void GetCombinations(int[] arr, int n, int k, int value, int index)
        {
            if (index > k - 1)
            {
                Console.WriteLine("({0})", string.Join(" ", arr));
                return;
            }

            for (int i = value; i <= n; i++)
            {
                arr[index] = i;
                GetCombinations(arr, n, k, i + 1, index + 1);
            }
        }
    }
}
