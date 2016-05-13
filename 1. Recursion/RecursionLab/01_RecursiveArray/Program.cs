using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_RecursiveArray
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = new int[] { 1, 2, 3, 4, 5, 6 };
            var result = FindSum(input, 0);
            Console.WriteLine(result);
        } 

        static int FindSum(int[] arr, int index)
        {
            if (index == arr.Length - 1)
            {
                return arr[index];
            }

            return arr[index] + FindSum(arr, index+1);
        }
    }
}
