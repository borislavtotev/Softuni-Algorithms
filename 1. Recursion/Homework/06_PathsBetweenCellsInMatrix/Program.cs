using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06_PathsBetweenCellsInMatrix
{
    class Program
    {
        static char[,] matrix;
        static List<string> foundPaths = new List<string>(); 

        static void Main(string[] args)
        {
            matrix = new char[,] {
                { 's', ' ', ' ', ' ', ' ', ' ' },
                { ' ', '*', '*', ' ', '*', ' ' },
                { ' ', '*', '*', ' ', '*', ' ' },
                { ' ', '*', 'e', ' ', ' ', ' ' },
                { ' ', ' ', ' ', '*', ' ', ' ' }
                //{ 's', ' ', ' ', ' ' },
                //{ ' ', '*', '*', ' ' },
                //{ ' ', '*', '*', ' ' },
                //{ ' ', '*', 'e', ' ' },
                //{ ' ', ' ', ' ', ' ' }
            };

            Cell start = FindStart();
            if (start == null)
            {
                Console.WriteLine("Unable to find the start cell");
                return;
            }

            Move(start.Row, start.Column, new List<string>(), "");
            foreach (var path in foundPaths)
            {
                Console.WriteLine(string.Join("", path));
            }

            Console.WriteLine("Total paths founded:{0}", foundPaths.Count);
        }

        static Cell FindStart()
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    if (matrix[row, col] == 's')
                    {
                        return new Cell(row, col);
                    }
                }
            }

            return null;
        }

        static void Move(int row, int column, List<string> directions, string direction)
        {
            var cell = new Cell(row, column);

            if (!cell.IsValid())
            {
                return;
            }

            if (cell.IsVisited())
            {
                return;
            }

            if (cell.IsExit())
            {
                directions.Add(direction);
                foundPaths.Add(string.Join("", directions));
                directions.RemoveAt(directions.Count - 1);
                return;
            }

            if (!cell.IsStart() && !cell.IsFree())
            {
                return;
            }

            Cell.visited.Add(cell);
            directions.Add(direction);
            Move(row, column + 1, directions, "R");
            Move(row, column - 1, directions, "L");
            Move(row - 1, column, directions, "U");
            Move(row + 1, column, directions, "D");
            directions.RemoveAt(directions.Count - 1);
            Cell.visited.RemoveAt(Cell.visited.Count - 1);
        }

        public class Cell
        {
            public static List<Cell> visited = new List<Cell>();

            public Cell(int row, int column)
            {
                this.Column = column;
                this.Row = row;
            }

            public int Row { get; set; }

            public int Column { get; set; }

            public bool IsStart()
            {
                if (matrix[this.Row, this.Column] == 's')
                {
                    return true;
                }

                return false;
            }

            public bool IsValid()
            {
                if (this.Row >= 0 && this.Row < matrix.GetLength(0) && 
                    this.Column >= 0 && this.Column < matrix.GetLength(1))
                {
                    return true;
                }

                return false;
            }

            public bool IsExit()
            {
                if (matrix[this.Row, this.Column] == 'e')
                {
                    return true;
                }

                return false;
            }

            public bool IsFree()
            {
                if (matrix[this.Row, this.Column] == ' ')
                {
                    return true;
                }

                return false;
            }

            public bool IsVisited()
            {
                if (visited.Any(c => c.Column == this.Column && c.Row == this.Row))
                {
                    return true;
                }

                return false;
            }
        }
    }
}
