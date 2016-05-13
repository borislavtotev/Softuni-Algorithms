using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04TowerOfHanoi
{
    class Program
    {
        private static int stepsTaken = 0;
        
        private static Stack<int> source;
        private static readonly Stack<int> destination = new Stack<int>();
        private static readonly Stack<int> spare = new Stack<int>();

        static void Main(string[] args)
        {
            int numbedOfDisks = 3;
            source = new Stack<int>(Enumerable.Range(1, numbedOfDisks).Reverse());
            PrintRods();
            MoveDisk(numbedOfDisks, source, destination, spare);
        }

        private static void MoveDisk(int bottomDisk, Stack<int> diskSource, Stack<int> diskDestination, Stack<int> diskSpare)
        {
            if (bottomDisk == 1)
            {
                stepsTaken++;
                diskDestination.Push(diskSource.Pop());
                Console.WriteLine($"Step #{stepsTaken}: Moved disk: {bottomDisk}");
                PrintRods();
            }
            else
            {
                MoveDisk(bottomDisk - 1, diskSource, diskSpare, diskDestination);
                diskDestination.Push(diskSource.Pop());
                stepsTaken++;
                Console.WriteLine($"Step #{stepsTaken}: Moved disk: {bottomDisk}");
                PrintRods();
                MoveDisk(bottomDisk - 1, diskSpare, diskDestination, diskSource);
            }
        }
        
        private static void PrintRods()
        {
            Console.WriteLine("Source: {0}", string.Join(", ", source.Reverse()));
            Console.WriteLine("Destination: {0}", string.Join(", ", destination.Reverse()));
            Console.WriteLine("Spare: {0}", string.Join(", ", spare.Reverse()));
            Console.WriteLine();
        }
    }
}
