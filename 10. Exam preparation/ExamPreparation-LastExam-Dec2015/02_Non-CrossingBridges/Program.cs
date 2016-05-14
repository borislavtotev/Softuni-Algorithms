using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_Non_CrossingBridges
{
    class Program
    {
        static int[] numbers;

        static void Main(string[] args)
        {
            numbers = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            Dictionary<int, int> tempHash = new Dictionary<int, int>();
            var result = new string[numbers.Length];
            int bridges = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                if (tempHash.ContainsKey(numbers[i]))
                {
                    bridges++;
                    var previousIndex = tempHash[numbers[i]];
                    result[previousIndex] = numbers[i].ToString();
                    result[i] = numbers[i].ToString();
                    tempHash = new Dictionary<int, int>();
                    tempHash.Add(numbers[i], i);
                }
                else
                {
                    tempHash.Add(numbers[i], i);
                    result[i] = "X";
                }
            }

            if (bridges == 0)
            {
                Console.WriteLine("No bridges found");
            }
            else if (bridges == 1)
            {
                Console.WriteLine("1 bridge found");
            }
            else
            {
                Console.WriteLine("{0} bridges found", bridges);
            }
            Console.WriteLine(string.Join(" ", result));
        }

    }
}
