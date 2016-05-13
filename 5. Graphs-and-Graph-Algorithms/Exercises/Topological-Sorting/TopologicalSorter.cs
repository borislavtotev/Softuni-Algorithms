using System;
using System.Collections.Generic;
using System.Linq;

public class TopologicalSorter
{
    private Dictionary<string, List<string>> graph;
    private HashSet<string> visitedNodes;
    private LinkedList<string> sortedNodes;
    private HashSet<string> cicleNodes;

    public TopologicalSorter(Dictionary<string, List<string>> graph)
    {
        this.graph = graph;
    }

    //public ICollection<string> TopSort()
    //{
    //    var predecessorsCount = new Dictionary<string, int>();
    //    foreach (var node in this.graph)
    //    {
    //        if (! predecessorsCount.ContainsKey(node.Key))
    //        {
    //            predecessorsCount[node.Key] = 0;
    //        }

    //        foreach (var child in node.Value)
    //        {
    //            if (! predecessorsCount.ContainsKey(child))
    //            {
    //                predecessorsCount[child] = 0;
    //            }

    //            predecessorsCount[child]++;
    //        }
    //    }

    //    var removedNodes = new List<string>();
    //    while (true)
    //    {
    //        string nodeToRemove = graph.Keys.FirstOrDefault(n => predecessorsCount[n] == 0);

    //        if (nodeToRemove == null)
    //        {
    //            break;
    //        }

    //        foreach (var childNode in graph[nodeToRemove])
    //        {
    //            predecessorsCount[childNode]--;
    //        }

    //        graph.Remove(nodeToRemove);
    //        removedNodes.Add(nodeToRemove);
    //    }

    //    if (graph.Count > 0)
    //    {
    //        throw new InvalidOperationException("A cicle detected in the graph");
    //    }

    //    return removedNodes; 
    //}

    public ICollection<string> TopSort()
    {
        this.visitedNodes = new HashSet<string>();
        this.sortedNodes = new LinkedList<string>();
        this.cicleNodes = new HashSet<string>();

        foreach (var node in this.graph.Keys)
        {
            TopSortDFS(node);
        }

        return this.sortedNodes;
    }

    private void TopSortDFS(string node)
    {
        if (this.cicleNodes.Contains(node))
        {
            throw new InvalidOperationException("A cicle detected in the graph");
        }

        if (!this.visitedNodes.Contains(node)) {
            this.visitedNodes.Add(node);
            this.cicleNodes.Add(node);

            if (graph.ContainsKey(node))
            {
                foreach (var child in graph[node])
                {
                    TopSortDFS(child);
                }
            }

            this.cicleNodes.Remove(node);
            this.sortedNodes.AddFirst(node);
        }
    }
}
