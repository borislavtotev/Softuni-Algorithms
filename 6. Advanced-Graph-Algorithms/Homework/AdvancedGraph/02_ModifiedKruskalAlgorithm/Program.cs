using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_ModifiedKruskalAlgorithm
{
    class Program
    {
        static List<Edge> edges;
        static Node[] nodes;
        static Node[] parent;
        static int nodeCount;

        static void Main(string[] args)
        {
            Console.Write("Nodes: ");
            nodeCount = int.Parse(Console.ReadLine());
            Console.Write("Edges: ");
            int edgeCount = int.Parse(Console.ReadLine());

            edges = new List<Edge>();
            nodes = new Node[nodeCount];
            for (int i = 0; i < edgeCount; i++)
            {
                var edgeParams = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

                var startNodeRoot = edgeParams[0];
                Node startNode = null;
                if (nodes[startNodeRoot] != null)
                {
                    startNode = nodes[startNodeRoot];
                }
                else
                {
                    startNode = new Node(startNodeRoot);
                    nodes[startNodeRoot] = startNode;
                }

                var endNodeRoot = edgeParams[1];
                Node endNode = null;
                if (nodes[endNodeRoot] != null)
                {
                    endNode = nodes[endNodeRoot];
                }
                else
                {
                    endNode = new Node(endNodeRoot);
                    nodes[endNodeRoot] = endNode;
                }

                edges.Add(new Edge(startNode, endNode, edgeParams[2]));
            }

            var minimumSpanningForest = ModifiedKruskal();

            Console.WriteLine("Minimum spanning forest weight: " +
                minimumSpanningForest.Sum(e => e.Weight));
            foreach (var edge in minimumSpanningForest)
            {
                Console.WriteLine(edge);
            }

        }

        static List<Edge> ModifiedKruskal()
        {
            edges.Sort();
            var minimumSpanningTree = new List<Edge>();

            foreach (var edge in edges)
            {
                var startNode = edge.StartNode;
                if (startNode.Parent.Value != edge.EndNode.Parent.Value)
                {
                    minimumSpanningTree.Add(edge);
                    var currentEndNodeParent = edge.EndNode.Parent;
                    foreach (var child in edge.EndNode.Parent.Childs)
                    {
                        startNode.Parent.Childs.Add(child);
                        child.Parent = startNode.Parent;
                    }
                    startNode.Parent.Childs.Add(currentEndNodeParent);
                    currentEndNodeParent.Parent = startNode.Parent;
                }
            }

            return minimumSpanningTree;
        }
    }

    public class Node
    {
        public Node(int root)
        {
            this.Value = root;
            this.Childs = new List<Node>();
            this.Parent = this;
        }

        public int Value { get; set; }

        public Node Parent { get; set; }

        public List<Node> Childs { get; set; }
    }
}
