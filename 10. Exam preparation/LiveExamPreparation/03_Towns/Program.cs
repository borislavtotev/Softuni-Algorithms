using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_Towns
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            long[] seq = new long[n];
            for (int i = 0; i < n; i++)
            {
                var elements = Console.ReadLine().Split(' ');
                seq[i] = long.Parse(elements[0]);
            }

            int[] lenASC = new int[n];
            for (int x = 0; x < seq.Length; x++)
            {
                lenASC[x] = 1;
                for (int i = 0; i <= x - 1; i++)
                    if (seq[i] < seq[x] && lenASC[i] + 1 > lenASC[x])
                        lenASC[x] = 1 + lenASC[i];
            }

            int[] lenDSC = new int[n];
            for (int x = seq.Length - 1; x >= 0; x--)
            {
                lenDSC[x] = 1;
                for (int i = seq.Length - 1; i >= x + 1; i--)
                    if (seq[i] < seq[x] && lenDSC[i] + 1 > lenDSC[x])
                        lenDSC[x] = 1 + lenDSC[i];
            }

            var maxLen = int.MinValue;
            for (int i = 0; i < n; i++)
            {
                var newLen = lenASC[i] + lenDSC[i];
                if (newLen > maxLen)
                {
                    maxLen = newLen;
                }
            }

            // -1 becuase the current town is counted twice
            Console.WriteLine(maxLen - 1);
        }
    }
}
