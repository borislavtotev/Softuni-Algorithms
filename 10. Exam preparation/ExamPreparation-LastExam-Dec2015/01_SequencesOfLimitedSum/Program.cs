using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_SequencesOfLimitedSum
{
    class Program
    {
        static int n;
        static StringBuilder builder = new StringBuilder();

        static void Main(string[] args)
        {
            n = int.Parse(Console.ReadLine());
            CalSeq(new List<int>(), 0);
            Console.WriteLine(builder.ToString());
        }

        static void CalSeq(List<int> list, int currentSum)
        {
            int i = 1;

            while (currentSum + i <= n)
            {
                list.Add(i);
                builder.AppendLine(string.Join(" ", list));
                currentSum += i;
                CalSeq(list, currentSum);
                list.RemoveAt(list.Count - 1);
                currentSum -= i;
                i++;
            }  
        }
    }
}
