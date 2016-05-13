using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_ReverseArray
{
    class Program
    {
        static void Main(string[] args)
        {
            var arr = new int[] { 1, 2, 3, 4, 5, 6 };
            PrintReversedArray(arr, arr.Length - 1);
        }

        static void PrintReversedArray(int[] arr, int index)
        {
            if (index == -1)
            {
                return;
            } else
            {
                Console.Write(arr[index]);
                PrintReversedArray(arr, index - 1);
            }
        }
    }
}
