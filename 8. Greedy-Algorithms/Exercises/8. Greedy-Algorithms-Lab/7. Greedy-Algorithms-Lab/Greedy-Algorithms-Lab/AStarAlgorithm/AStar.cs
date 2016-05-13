namespace AStarAlgorithm
{
    using System;
    using System.Collections.Generic;

    public class AStar
    {
        private readonly PriorityQueue<Node> openNodeByFcost;
        private readonly HashSet<Node> closedSet;
        private readonly char[,] map;
        private readonly Node[,] graph;

        public AStar(char[,] map)
        {
            this.map = map;
            this.openNodeByFcost = new PriorityQueue<Node>();
            this.closedSet = new HashSet<Node>();
            this.graph = new Node[map.GetLength(0), map.GetLength(1)];
        }

        public List<int[]> FindShortestPath(int[] startCoords, int[] endCoords)
        {
            var startNode = this.GetNode(startCoords[0], startCoords[1]);
            startNode.GCost = 0;
            this.openNodeByFcost.Enqueue(startNode);

            while (openNodeByFcost.Count > 0)
            {
                var currentNode = openNodeByFcost.ExtractMin();
                this.closedSet.Add(currentNode);
                if (currentNode.Row == endCoords[0] && currentNode.Col == endCoords[1])
                {
                    return ReconstructPath(currentNode);
                }

                var neighbors = this.GetNeighbours(currentNode);
                foreach (var neighbor in neighbors)
                {
                    if (this.closedSet.Contains(neighbor))
                    {
                        continue;
                    }

                    var gCost = currentNode.GCost + CalculateGCost(neighbor, currentNode);
                    if (gCost < neighbor.GCost)
                    {
                        neighbor.GCost = gCost;
                        neighbor.Parent = currentNode;

                        if (!this.openNodeByFcost.Contains(neighbor))
                        {
                            neighbor.HCost = CalculateHCost(neighbor, endCoords);
                            this.openNodeByFcost.Enqueue(neighbor);
                        }
                        else
                        {
                            this.openNodeByFcost.DecreaseKey(neighbor);
                        }
                    }
                }
            }

            return new List<int[]>(0);
        }

        private static List<int[]> ReconstructPath(Node currentNode)
        {
            var cells = new List<int[]>();
            while (currentNode != null)
            {
                cells.Add(new[] { currentNode.Row, currentNode.Col });
                currentNode = currentNode.Parent;
            }

            return cells;
        }

        private static int CalculateHCost(Node node, int[] endCoords)
        {
            return GetDistance(node.Row, node.Col, endCoords[0], endCoords[1]);
        }

        private static int CalculateGCost(Node node, Node prev)
        {
            return GetDistance(node.Row, node.Col, prev.Row, prev.Col);
        }

        private static int GetDistance(int r1, int c1, int r2, int c2)
        {
            var deltaX = Math.Abs(r1 - r2);
            var deltaY = Math.Abs(c1 - c2);
            if (deltaX > deltaY)
            {
                return 14 * deltaY + 10 * (deltaX - deltaY);
            }

            return 14 * deltaX + 10 * (deltaY - deltaX);
        }

        private Node GetNode(int row, int col)
        {
            return this.graph[row, col] ?? (this.graph[row, col] = new Node(row, col));
        }

        private List<Node> GetNeighbours(Node node)
        {
            var neighboues = new List<Node>();
            var maxRow = this.graph.GetLength(0);
            var maxCol = this.graph.GetLength(1);
            for (int row = node.Row-1; row <= node.Row + 1 && row < maxRow; row++)
            {
                if (row < 0) continue;
                for (int col = node.Col - 1; col <= node.Col + 1 && col < maxCol; col++)
                {
                    if (col < 0 || map[row, col] == 'W') continue;
                    var neighbour = this.GetNode(row, col);
                    neighboues.Add(neighbour);
                }
            }

            return neighboues;
        }
    }
}
