using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_KnightTour
{
    class Program
    {
        static bool[,] visited;
        static int size;
        static int[,] result;

        static void Main(string[] args)
        {
            size = int.Parse(Console.ReadLine());
            visited = new bool[size, size];
            result = new int[size, size];

            int startRow = 0;
            int startColumn = 0;
            int visitedCells = 0;
            while (visitedCells < size * size)
            {
                visitedCells++;
                visited[startRow, startColumn] = true;
                result[startRow, startColumn] = visitedCells;

                var possibleCells = GetNextKnightTurn(startRow, startColumn);
                var minimumNumberOfTurns = int.MaxValue;
                Tuple<int, int> nextCell = null;
                foreach (var cell in possibleCells)
                {
                    var newPossibleCells = GetNextKnightTurn(cell.Item1, cell.Item2);
                    if (newPossibleCells.Count < minimumNumberOfTurns)
                    {
                        minimumNumberOfTurns = newPossibleCells.Count;
                        nextCell = cell;
                    }
                }

                if (nextCell != null)
                {
                    startRow = nextCell.Item1;
                    startColumn = nextCell.Item2;
                }
                else
                {
                    break;
                }
            }

            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    Console.Write(result[row, col].ToString().PadLeft(5));
                }
                Console.WriteLine();
            }
        }

        static List<Tuple<int, int>> GetNextKnightTurn(int startRow, int startColumn)
        {
            var currentList = new List<Tuple<int, int>>();

            if (startColumn + 2 < size)
            {
                if (startRow + 1 < size && !visited[startRow + 1, startColumn + 2])
                {
                    currentList.Add(new Tuple<int, int>(startRow + 1, startColumn + 2));
                }

                if (startRow - 1 >= 0 && !visited[startRow - 1, startColumn + 2])
                {
                    currentList.Add(new Tuple<int, int>(startRow - 1, startColumn + 2));
                }
            }

            if (startColumn - 2 >= 0)
            {
                if (startRow - 1 >= 0 && !visited[startRow - 1, startColumn - 2])
                {
                    currentList.Add(new Tuple<int, int>(startRow - 1, startColumn - 2));
                }

                if (startRow + 1 < size && !visited[startRow + 1, startColumn - 2])
                {
                    currentList.Add(new Tuple<int, int>(startRow + 1, startColumn - 2));
                }
            }

            if (startRow + 2 < size)
            {
                if (startColumn - 1 >= 0 && !visited[startRow + 2, startColumn - 1])
                {
                    currentList.Add(new Tuple<int, int>(startRow + 2, startColumn - 1));
                }

                if (startColumn + 1 < size && !visited[startRow + 2, startColumn + 1])
                {
                    currentList.Add(new Tuple<int, int>(startRow + 2, startColumn + 1));
                }
            }

            if (startRow - 2 >= 0)
            {
                if (startColumn + 1 < size && !visited[startRow - 2, startColumn + 1])
                {
                    currentList.Add(new Tuple<int, int>(startRow - 2, startColumn + 1));
                }

                if (startColumn - 1 >= 0 && !visited[startRow - 2, startColumn - 1])
                {
                    currentList.Add(new Tuple<int, int>(startRow - 2, startColumn - 1));
                }
            }

            return currentList;
        }
    }
}
