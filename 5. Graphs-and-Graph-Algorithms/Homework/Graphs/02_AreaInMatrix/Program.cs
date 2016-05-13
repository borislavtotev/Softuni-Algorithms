using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_AreaInMatrix
{
    class Program
    {
        static string[,] matrix;
        static bool[,] visited;
        static Dictionary<string, int> areas;
         
        static void Main(string[] args)
        {
            //matrix = new string[,]{
            //    { "a", "a", "c", "c", "c", "a", "a", "c" },
            //    { "b", "a", "a", "a", "a", "c", "c", "c" },
            //    { "b", "a", "a", "b", "a", "c", "c", "c" },
            //    { "b", "b", "d", "a", "a", "c", "c", "c" },
            //    { "c", "c", "d", "c", "c", "c", "c", "c" },
            //    { "c", "c", "d", "c", "c", "c", "c", "c" }
            //};

            //matrix = new string[,]{
            //    { "a", "a", "a" },
            //    { "a", "a", "a" },
            //    { "a", "a", "a" }
            //};

            matrix = new string[,]{
                { "a", "s", "s", "s", "a", "a", "d", "a", "s" },
                { "a", "d", "s", "d", "a", "s", "d", "a", "d" },
                { "s", "d", "s", "d", "a", "d", "s", "a", "s" },
                { "s", "d", "a", "s", "d", "s", "d", "s", "a" },
                { "s", "s", "s", "s", "a", "s", "d", "d", "d" }
            };

            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);

            visited = new bool[rows, columns];
            areas = new Dictionary<string, int>();

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    if (!visited[row, column])
                    {
                        var cell = matrix[row, column];
                        Traverse(cell, row, column);

                        if (!areas.ContainsKey(cell))
                        {
                            areas.Add(cell, 0);
                        }

                        areas[cell]++;
                    }
                }
            }

            Console.WriteLine("Areas: {0}", areas.Select(a => a.Value).Sum());
            foreach (var area in areas.Keys)
            {
                Console.WriteLine("Letter '{0}' -> {1}", area, areas[area]);
            }
        }

        static void Traverse(string lastKey, int row, int column)
        {
            if (visited[row, column])
            {
                return;
            }

            string currentKey = matrix[row, column];

            if (currentKey != lastKey)
            {
                return;
            }

            visited[row, column] = true;
            if (row - 1 >= 0)
            {
                Traverse(currentKey, row - 1, column);
            }

            if (row + 1 < matrix.GetLength(0))
            {
                Traverse(currentKey, row + 1, column);
            }

            if (column - 1 >= 0)
            {
                Traverse(currentKey, row, column - 1);
            }

            if (column + 1 < matrix.GetLength(1))
            {
                Traverse(currentKey, row, column + 1);
            }
        }
    }
}
