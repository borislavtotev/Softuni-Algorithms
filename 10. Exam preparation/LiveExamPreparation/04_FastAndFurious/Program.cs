using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04_FastAndFurious
{
    class Program
    {
        const double Inf = double.PositiveInfinity;

        static void Main()
        {
            // for the Roads:
            Console.ReadLine();

            // Get the towns and distances
            Dictionary<Tuple<int, int>, double> roadMap = new Dictionary<Tuple<int, int>, double>();
            while (true)
            {
                var input = Console.ReadLine();
                if (input == "Records:")
                {
                    break;
                }

                var inputParams = input.Split(' ');
                var town1Id = Town.GetId(inputParams[0]);
                var town2Id = Town.GetId(inputParams[1]);
                double minTime = double.Parse(inputParams[2]) / double.Parse(inputParams[3]);
                roadMap.Add(new Tuple<int, int>(town1Id, town2Id), minTime);
            }

            // get the records
            var records = new Dictionary<string, List<KeyValuePair<DateTime, int>>>();

            while (true)
            {
                var input = Console.ReadLine();
                if (input == "End")
                {
                    break;
                }

                var inputParams = input.Split(' ').ToArray();
                var townId = Town.GetId(inputParams[0]);
                var label = inputParams[1];
                DateTime dateTime = DateTime.ParseExact(inputParams[2], "HH:mm:ss", CultureInfo.InvariantCulture);
                if (!records.ContainsKey(label))
                {
                    records.Add(label, new List<KeyValuePair<DateTime, int>>());
                }

                records[label].Add(new KeyValuePair<DateTime, int>(dateTime, townId));
            }

            double[,] graph = new double[Town.townsCount, Town.townsCount];

            foreach (var road in roadMap.Keys)
            {
                graph[road.Item1, road.Item2] = roadMap[road];
                graph[road.Item2, road.Item1] = roadMap[road];
            }

            var dist = new double[Town.townsCount, Town.townsCount];
            for (int i = 0; i < Town.townsCount; i++)
            {
                for (int j = 0; j < Town.townsCount; j++)
                {
                    if (graph[i, j] == 0)
                    {
                        dist[i, j] = Inf;
                    }
                    else
                    {
                        dist[i, j] = graph[i, j];
                    }
                }
            }

            var v = graph.GetLength(0);
            for (int k = 0; k < v; k++)
            {
                for (int i = 0; i < v; i++)
                {
                    for (int j = 0; j < v; j++)
                    {
                        if (dist[i, k] + dist[k, j] < dist[i, j])
                        {
                            dist[i, j] = dist[i, k] + dist[k, j];
                        }
                    }
                }
            }
            var results = new List<string>();
            foreach (var label in records.Keys)
            {
                
                var timeAndTowns = records[label].OrderBy(k => k.Key).ToList();
                if (timeAndTowns.Count() > 1)
                {
                    for (int i = 0; i < timeAndTowns.Count - 1; i++)
                    {
                        var time = timeAndTowns[i + 1].Key - timeAndTowns[i].Key;
                        var timeInSeconds = time.TotalSeconds;
                        var town1Id = timeAndTowns[i].Value;
                        var town2Id = timeAndTowns[i + 1].Value;

                        if (timeInSeconds == 0)
                        {
                            results.Add(label);
                            break;
                        }

                        if (dist[town1Id, town2Id] == Inf || town1Id == town2Id)
                        {
                            continue;
                        }

                        var minTime = dist[town1Id, town2Id] * 60 * 60;
                        if (minTime > timeInSeconds)
                        {
                            results.Add(label);
                            break;
                        }
                    }
                }
            }

            results.Sort();
            foreach (var result in results)
            {
                Console.WriteLine(result);
            }
        }

        public class Town
        {
            public static int townsCount = 0;
            static HashSet<Town> towns = new HashSet<Town>();

            public Town(string name)
            {
                this.Id = townsCount;
                townsCount++;
                this.Name = name;
                towns.Add(this);
            }

            public int Id { get; private set; }
            public string Name { get; private set; }

            public static int GetId(string name)
            {
                var currentTown = towns.FirstOrDefault(t => t.Name == name);
                if (currentTown != null)
                {
                    return currentTown.Id;
                }

                var newTown = new Town(name);
                return newTown.Id;
            }
        }
    }
}