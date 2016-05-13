using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04_ShortestPathsBetweenAllNodes
{
    class Program
    {
        static int[,] graph;

        static void Main(string[] args)
        {
            Console.Write("Nodes: ");
            int nodeCount = int.Parse(Console.ReadLine());

            Console.Write("Edges: ");
            int edgesCount = int.Parse(Console.ReadLine());

            graph = new int[nodeCount, nodeCount];
            for (int i = 0; i < edgesCount; i++)
            {
                var edgeParams = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                graph[edgeParams[0], edgeParams[1]] = edgeParams[2];
                graph[edgeParams[1], edgeParams[0]] = edgeParams[2];
            }

            var resultMatrix = new int[nodeCount, nodeCount];
            for (int sourceNode = 0; sourceNode < nodeCount; sourceNode++)
            {
                for (int destinationNode = 0; destinationNode < sourceNode; destinationNode++)
                {
                    var path = Dijkstra(sourceNode, destinationNode);
                    int pathLength = 0;
                    for (int i = 0; i < path.Count - 1; i++)
                    {
                        pathLength += graph[path[i], path[i + 1]];
                    }

                    resultMatrix[sourceNode, destinationNode] = pathLength;
                    resultMatrix[destinationNode, sourceNode] = pathLength;
                }
            }

            Console.WriteLine("Shortest paths matrix:");
            var dashes = "";
            for (int i = 0; i < nodeCount; i++)
            {
                Console.Write(i + " ");
                dashes += "-";
            }
            Console.WriteLine();
            Console.WriteLine(dashes);

            for (int i = 0; i < nodeCount; i++)
            {
                for (int j = 0; j < nodeCount; j++)
                {
                    Console.Write(resultMatrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        static List<int> Dijkstra(int sourceNode, int destinationNode)
        {
            int n = graph.GetLength(0);

            // Initialize the distance[]
            int[] distance = new int[n];
            for (int i = 0; i < n; i++)
            {
                distance[i] = int.MaxValue;
            }
            distance[sourceNode] = 0;

            // Dijkstra's algorithm implemented without priority queue
            var used = new bool[n];
            int?[] previous = new int?[n];
            while (true)
            {
                // Find the nearest unused node from the source
                int minDistance = int.MaxValue;
                int minNode = 0;
                for (int node = 0; node < n; node++)
                {
                    if (!used[node] && distance[node] < minDistance)
                    {
                        minDistance = distance[node];
                        minNode = node;
                    }
                }
                if (minDistance == int.MaxValue)
                {
                    // No min distance node found --> algorithm finished
                    break;
                }

                used[minNode] = true;

                // Improve the distance[0…n-1] through minNode
                for (int i = 0; i < n; i++)
                {
                    if (graph[minNode, i] > 0)
                    {
                        int newDistance = distance[minNode] + graph[minNode, i];
                        if (newDistance < distance[i])
                        {
                            distance[i] = newDistance;
                            previous[i] = minNode;
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
