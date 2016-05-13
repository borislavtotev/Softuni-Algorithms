using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_CyclesInAGraph
{
    class Program
    {
        static Dictionary<string, List<string>> graph;
        static HashSet<string> visited;
        static bool cyclic = false;
        static HashSet<string> cicleNodes;


        static void Main(string[] args)
        {
            //var graphStrings = new string[]
            //{
            //    "K – X",
            //    "X – Y",
            //    "X – N",
            //    "N – J",
            //    "M – N",
            //    "A – Z",
            //    "B – P",
            //    "I – F",
            //    "A – Y",
            //    "Y – L",
            //    "M – I",
            //    "F – P",
            //    "Z – E",
            //    "P – E"
            //};

            //var graphStrings = new string[]
            //{
            //    "K – J",
            //    "J – N",
            //    "N – L",
            //    "N – M",
            //    "M – I"
            //};

            //var graphStrings = new string[]
            //{
            //    "E – Q",
            //    "Q – P",
            //    "P – B"
            //};

            //var graphStrings = new string[]
            //{
            //    "A – F",
            //    "F – D",
            //    "D – A"
            //};

            var graphStrings = new string[]
            {
                "C – G",
            };

            graph = new Dictionary<string, List<string>>();
            visited = new HashSet<string>();
            cicleNodes = new HashSet<string>();

            foreach (var item in graphStrings)
            {
                var elements = item.Split(' ');
                var parent = elements[0];
                var child = elements[2];
                if (!graph.ContainsKey(parent))
                {
                    graph.Add(parent, new List<string>());
                }

                graph[parent].Add(child);
            }

            foreach (var node in graph.Keys)
            {
                DFS(node);
                if (cyclic)
                {
                    break;
                }
            }


            Console.Write("Acyclic: ");
            if (cyclic)
            {
                Console.WriteLine("No");
            }
            else
            {
                Console.WriteLine("Yes");
            }
        }

        static void DFS(string node)
        {
            if (cyclic)
            {
                return;
            }

            if (cicleNodes.Contains(node))
            {
                cyclic = true;
                return;
            }

            visited.Add(node);
            cicleNodes.Add(node);
            if (graph.ContainsKey(node))
            {
                foreach (var childNode in graph[node])
                {
                    DFS(childNode);
                }
            }

            cicleNodes.Remove(node);
        }
    }
}
