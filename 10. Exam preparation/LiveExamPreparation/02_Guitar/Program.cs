using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _02_Guitar
{
    class Program
    {
        static void Main(string[] args)
        {
            var levelChanges = Regex.Split(Console.ReadLine(), "[^\\d]+").Select(int.Parse).ToArray();
            int startVolume = int.Parse(Console.ReadLine());
            int maxVolume = int.Parse(Console.ReadLine());

            var lastSongLevels = new HashSet<int>();
            lastSongLevels.Add(startVolume);

            var maxChange = levelChanges.Sum();
            if (startVolume + maxChange < maxVolume)
            {
                Console.WriteLine(startVolume + maxChange);
                return;
            }

            for (int i = 0; i < levelChanges.Count(); i++)
            {
                var foundNewLevel = false;
                var newSongLevels = new HashSet<int>();

                foreach (var previousVolume in lastSongLevels)
                {
                    var smaller = previousVolume - levelChanges[i];
                    if (smaller >= 0)
                    {
                        newSongLevels.Add(smaller);
                        foundNewLevel = true;
                    }

                    var bigger = previousVolume + levelChanges[i];
                    if (bigger <= maxVolume)
                    {
                        newSongLevels.Add(bigger);
                        foundNewLevel = true;
                    }
                }

                if (!foundNewLevel)
                {
                    Console.WriteLine("-1");
                    return;
                }

                lastSongLevels = newSongLevels;
            }

            Console.WriteLine(lastSongLevels.Max());
        }
    }
}
