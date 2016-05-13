using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_NestedLoopsToRecursion
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            PrintWithRecursion(new int[n], n, 1, 1);
        }

        static void PrintWithRecursion(int[] arr, int length, int position, int startValue)
        {
            if (position > length)
            {
                Console.WriteLine(string.Join(", ", arr));
                return;
            }
          
            for (int i = startValue; i <= length; i++)
            {
                arr[position - 1] = i;
                PrintWithRecursion(arr, length, position + 1, 1);
            }
        }
    }
}
