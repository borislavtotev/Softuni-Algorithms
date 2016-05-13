using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_ExtendCableNetwork
{
    class Program
    {
        static List<int> connected;
        static List<Edge> notConnected;
        static List<Edge> newConnections;
        static int budgetUsed; 

        static void Main(string[] args)
        {
            Console.Write("Budget: ");
            var budget = int.Parse(Console.ReadLine());

            Console.Write("Nodes: ");
            var nodes = int.Parse(Console.ReadLine());

            Console.Write("Edges: ");
            var edges = int.Parse(Console.ReadLine());

            connected = new List<int>();
            notConnected = new List<Edge>();
            newConnections = new List<Edge>();

            for (int i = 0; i < edges; i++)
            {
                var inputParams = Console.ReadLine().Split(' ');
                var newEdge = new Edge(int.Parse(inputParams[0]), int.Parse(inputParams[1]), int.Parse(inputParams[2]));
                if (inputParams.Count() == 4)
                {
                    connected.Add(newEdge.StartNode);
                    connected.Add(newEdge.EndNode);
                }
                else
                {
                    notConnected.Add(newEdge);
                }
            }

            BuildNewConnections(budget);

            foreach (var newConnection in newConnections)
            {
                Console.WriteLine(newConnection);
            }
            Console.WriteLine("Budget used: " + budgetUsed);

        }

        static void BuildNewConnections(int budget)
        {
            notConnected.Sort();
            int startIndex = 0;
            while (startIndex < notConnected.Count && budget > 0)
            {
                var cheepestConnection = notConnected[startIndex];
                if ((connected.Contains(cheepestConnection.StartNode) ^ connected.Contains(cheepestConnection.EndNode))
                    && (cheepestConnection.Weight <= budget))
                {
                    int newConnectedNode = connected.Contains(cheepestConnection.StartNode) ? cheepestConnection.EndNode : cheepestConnection.StartNode;
                    connected.Add(newConnectedNode);
                    newConnections.Add(cheepestConnection);
                    notConnected.Remove(cheepestConnection);
                    budget -= cheepestConnection.Weight;
                    budgetUsed += cheepestConnection.Weight;
                    startIndex = 0;
                }
                else
                {
                    startIndex++;
                }
            }
        }
     }
}
