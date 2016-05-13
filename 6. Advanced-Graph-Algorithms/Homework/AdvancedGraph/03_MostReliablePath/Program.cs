using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_MostReliablePath
{
    class Program
    {
        static int[,] graph;

        static void Main(string[] args)
        {
            Console.Write("Nodes: ");
            int nodeCount = int.Parse(Console.ReadLine());

            Console.Write("Path: ");
            var pathArgs = Console.ReadLine().Split(' ');
            int startNode = int.Parse(pathArgs[0]);
            int endNode = int.Parse(pathArgs[2]);

            Console.Write("Edges: ");
            int edgesCount = int.Parse(Console.ReadLine());

            graph = new int[nodeCount, nodeCount];
            for (int i = 0; i < edgesCount; i++)
            {
                var edgeParams = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                graph[edgeParams[0], edgeParams[1]] = edgeParams[2];
                graph[edgeParams[1], edgeParams[0]] = edgeParams[2];
            }

            var path = Dijkstra(startNode, endNode);
            if (path == null)
            {
                Console.WriteLine("no path");
            }
            else
            {
                decimal pathLength = 1;
                for (int i = 0; i < path.Count - 1; i++)
                {
                    pathLength *= graph[path[i], path[i + 1]] / 100m;
                }
                var formattedPath = string.Join("->", path);
                pathLength = Math.Round(pathLength * 100m, 2);
                Console.WriteLine("Most reliable path reliability: {0}%", pathLength);
                Console.WriteLine(formattedPath);
            }
        }

        static List<int> Dijkstra(int sourceNode, int destinationNode)
        {
            int n = graph.GetLength(0);

            // Initialize the distance[]
            decimal[] distance = new decimal[n];
            for (int i = 0; i < n; i++)
            {
                distance[i] = decimal.MinValue;
            }
            distance[sourceNode] = 1;

            // Dijkstra's algorithm implemented without priority queue
            var used = new bool[n];
            int?[] previous = new int?[n];
            while (true)
            {
                // Find the nearest unused node from the source
                decimal minDistance = decimal.MinValue;
                int maxNode = -1;
                for (int node = 0; node < n; node++)
                {
                    if (!used[node] && distance[node] > minDistance)
                    {
                        minDistance = distance[node];
                        maxNode = node;
                    }
                }
                if (minDistance == decimal.MinValue)
                {
                    // No min distance node found --> algorithm finished
                    break;
                }

                used[maxNode] = true;

                // Improve the distance[0…n-1] through minNode
                for (int i = 0; i < n; i++)
                {
                    if (graph[maxNode, i] > 0)
                    {
                        decimal newDistance = 0;
                        //if (distance[maxNode] == 0)
                        //{
                        //    newDistance = graph[maxNode, i] / 100m;
                        //}
                        //else
                        //{
                            newDistance = distance[maxNode] * graph[maxNode, i] / 100m;
                        //}

                        if (newDistance > distance[i])
                        {
                            distance[i] = newDistance;
                            previous[i] = maxNode;
                        }
                    }
                }
            }

            if (distance[destinationNode] == int.MaxValue)
            {
                // No path found from sourceNode to destinationNode
                return null;
            }

            // Reconstruct the shortest path from sourceNode to destinationNode
            var path = new List<int>();
            int? currentNode = destinationNode;
            while (currentNode != null)
            {
                path.Add(currentNode.Value);
                currentNode = previous[currentNode.Value];
            }
            path.Reverse();
            return path;
        }
    }
}
