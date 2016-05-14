using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06_Renewal
{
    class Program
    {
        static int[,] graph;
        static int[,] buildingGraph;
        static int[,] destructiveGraph;
        static int n;

        static void Main()
        {
            n = int.Parse(Console.ReadLine());
            graph = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                var inputElements = Console.ReadLine().ToArray();
                for (int j = 0; j < n; j++)
                {
                    graph[i, j] = int.Parse(inputElements[j].ToString());
                }
            }

            buildingGraph = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                var inputElements = Console.ReadLine().ToArray();
                for (int j = 0; j < n; j++)
                {
                    buildingGraph[i, j] = GetValue(inputElements[j]);
                }
            }

            destructiveGraph = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                var inputElements = Console.ReadLine().ToArray();
                for (int j = 0; j < n; j++)
                {
                    destructiveGraph[i, j] = GetValue(inputElements[j]);
                }
            }

            var spanningTreeNodes = new HashSet<int>();
            var spannngTreeEdges = new List<Edge>();
            var removedTreeEdges = new List<Edge>();

            // Start Prim's algorithm from each node not still in the spanning tree
            for (int i = 0; i < n; i++)
            {
                if (!spanningTreeNodes.Contains(i))
                {
                    Prim(graph, spanningTreeNodes, i, spannngTreeEdges, removedTreeEdges);
                }
            }

            var totalSum = spannngTreeEdges.Sum(e => e.Weight);
            foreach (var removedEdge in removedTreeEdges)
            {
                if (!spannngTreeEdges.Contains(removedEdge))
                {
                    totalSum += removedEdge.Weight;
                }
            }
            Console.WriteLine(totalSum);
        }

        private static void Prim(int[,] graph,
            HashSet<int> spanningTreeNodes, int startNode,
            List<Edge> spannngTreeEdges, List<Edge> removedTreeEdges)
        {
            spanningTreeNodes.Add(startNode);
            var priorityExistingEdgesQueue = new BinaryHeap<Edge>();
            for (int j = 0; j < n; j++)
            {
                if (graph[startNode, j] == 1)
                {
                    var edge = new Edge(startNode, j, destructiveGraph[startNode, j]);
                    priorityExistingEdgesQueue.Enqueue(edge);
                }
            }

            while (priorityExistingEdgesQueue.Count > 0)
            {
                var smallestEdge = priorityExistingEdgesQueue.ExtractMin();
                removedTreeEdges.Add(smallestEdge);
                graph[smallestEdge.StartNode, smallestEdge.EndNode] = 0;
                graph[smallestEdge.EndNode, smallestEdge.StartNode] = 0;
            }

            if (priorityExistingEdgesQueue.Count == 0)
            {
                var priorityQueue = new BinaryHeap<Edge>();

                for (int j = 0; j < n; j++)
                {
                    var edge = new Edge(startNode, j, buildingGraph[startNode, j]);
                    priorityQueue.Enqueue(edge);
                }

                while (priorityQueue.Count > 0)
                {
                    var smallestEdge = priorityQueue.ExtractMin();
                    if (spanningTreeNodes.Contains(smallestEdge.StartNode) ^
                        spanningTreeNodes.Contains(smallestEdge.EndNode))
                    {
                        var nonTreeNode = spanningTreeNodes.Contains(smallestEdge.StartNode)
                            ? smallestEdge.EndNode
                            : smallestEdge.StartNode;
                        spannngTreeEdges.Add(smallestEdge);
                        spanningTreeNodes.Add(nonTreeNode);

                        for (int j = 0; j < n; j++)
                        {
                            var edge = new Edge(nonTreeNode, j, buildingGraph[nonTreeNode, j]);
                            if (edge != smallestEdge)
                            {
                                priorityQueue.Enqueue(edge);
                            }
                        }
                    }
                }
            }
        }

        static int GetValue(char c)
        {
            if (c >= 'A' && c <= 'Z')
                return c - 'A';
            return c - 'a' + 26;
        }
    }
}
