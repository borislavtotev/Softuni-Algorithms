using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04_ShortestPathBetweenNodesFloydW
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

            var resultMatrix = new double[nodeCount, nodeCount];
            for (int i = 0; i < resultMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < resultMatrix.GetLength(1); j++)
                {
                    if (graph[i, j] != 0)
                    {
                        resultMatrix[i, j] = graph[i, j];
                    }
                    else if (i != j)
                    {
                        resultMatrix[i, j] = double.PositiveInfinity;
                    }
                }
            }

            int v = resultMatrix.GetLength(0);
            for (int k = 0; k < v; k++)
            {
                for (int i = 0; i < v; i++)
                {
                    for (int j = 0; j < v; j++)
                    {
                        if (resultMatrix[i, k] + resultMatrix[k, j] < resultMatrix[i, j])
                        {
                            resultMatrix[i, j] = resultMatrix[i, k] + resultMatrix[k, j];
                        }
                    }
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
    }
}
