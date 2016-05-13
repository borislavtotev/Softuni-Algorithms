using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_DistanceBetweenVertice
{
    class Program
    {
        static Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>()
        {
            {11, new List<int>() { 4 }},
            {4, new List<int>() {12, 1 }},
            {1, new List<int>() {12, 21, 7}},
            {7, new List<int>() {21}},
            {12, new List<int>() {4, 19}},
            {19, new List<int>() {1, 21}},
            {21, new List<int>() {14, 31}},
            {14, new List<int>() {14}},
            {31, new List<int>() {}}
        };

        static List<Tuple<int, int>> distances = new List<Tuple<int, int>>()
        {
            new Tuple<int, int>(11, 7),
            new Tuple<int, int>(11, 21),
            new Tuple<int, int>(21, 4),
            new Tuple<int, int>(19, 14),
            new Tuple<int, int>(1, 4),
            new Tuple<int, int>(1, 11),
            new Tuple<int, int>(31, 21),
            new Tuple<int, int>(11, 14)
        };

        static void Main(string[] args)
        {
            foreach (var distance in distances)
            {
                var parent = distance.Item1;
                var child = distance.Item2;
                var steps = 0;
                var childFound = false;
                Node<int> foundedChild = new Node<int>();
                HashSet<Node<int>> visited = new HashSet<Node<int>>();

                var queue = new Queue<Node<int>>();
                queue.Enqueue(new Node<int>(parent));
                
                if (parent == child)
                {
                    Console.WriteLine("{0}, {1} -> {2}", parent, child, 1);
                    continue;
                }

                while (queue.Count > 0 && !childFound)
                {
                    var node = queue.Dequeue();

                    if (graph.ContainsKey(node.Value))
                    {
                        visited.Add(node);

                        if (graph[node.Value].Count > 0)
                        {   
                            foreach (var childNode in graph[node.Value])
                            {
                                var newNode = new Node<int>(childNode);
                                newNode.Parent = node;

                                if (!visited.Any(n => n.Value.CompareTo(newNode.Value) == 0))
                                {
                                    //Console.WriteLine(childNode);
                                    if (childNode == child)
                                    {
                                        childFound = true;
                                        foundedChild = newNode;
                                        break;
                                    }

                                    queue.Enqueue(newNode);
                                }
                            }
                        }

                    }
                }

                if (!childFound)
                {
                    steps = -1;
                }
                else
                {
                    Node<int> currentNode = foundedChild;
                    steps = 0;
                    while (currentNode.Parent != null)
                    {
                        currentNode = currentNode.Parent;
                        steps++;
                    };
                }
                    
                Console.WriteLine("{0}, {1} -> {2}", parent, child, steps);
            }
        }

        public class Node<T> where T: IComparable<T>
        {
            public Node()
            {

            }

            public Node(T value)
            {
                this.Value = value;
            }

            public T Value { get; private set; }

            public Node<T> Parent { get; set; }

            public int CompareTo(Node<T> other)
            {
                return this.Value.CompareTo(other.Value);
            }
        }

    }
}
