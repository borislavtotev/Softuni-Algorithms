using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _07_ConnectedAreasInAMatrix
{
    class Program
    {
        static char[,] matrix;
        static SortedSet<Area> foundAreas = new SortedSet<Area>();

        static void Main(string[] args)
        {
            matrix = new char[,] {
                //{ ' ', ' ', ' ', '*', ' ', ' ', ' ', '*', ' ' },
                //{ ' ', ' ', ' ', '*', ' ', ' ', ' ', '*', ' ' },
                //{ ' ', ' ', ' ', '*', ' ', ' ', ' ', '*', ' ' },
                //{ ' ', ' ', ' ', ' ', '*', ' ', '*', ' ', ' ' }
                { '*', ' ', ' ', '*', ' ', ' ', ' ', '*', ' ', ' ' },
                { '*', ' ', ' ', '*', ' ', ' ', ' ', '*', ' ', ' ' },
                { '*', ' ', ' ', '*', '*', '*', '*', '*', ' ', ' ' },
                { '*', ' ', ' ', '*', ' ', ' ', ' ', '*', ' ', ' ' },
                { '*', ' ', ' ', '*', ' ', ' ', ' ', '*', ' ', ' ' }
            };

            FindAreas();

            Console.WriteLine("Total areas found:{0}", foundAreas.Count);
            var i = 0;
            foreach (var area in foundAreas)
            {
                i++;
                Console.WriteLine("Area #{0} at ({1}, {2}), size: {3}", i, area.Position.Row, area.Position.Column, area.Size);
            }
        }
        
        static void FindAreas()
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    var cell = new Cell(i, j);
                    if (!cell.IsVisited() && cell.IsFree())
                    {
                        var area = new Area(cell);
                        TraverseNewArea(area);
                    }
                }
            }
        }

        static void TraverseNewArea(Area area)
        {
            area.Size = Move(area.Position.Row, area.Position.Column, 0);
            foundAreas.Add(area);
        }

        static int Move(int row, int column, int size)
        {
            var cell = new Cell(row, column);

            if (!cell.IsValid())
            {
                return size;
            }

            if (cell.IsVisited())
            {
                return size;
            }

            if (!cell.IsFree())
            {
                return size;
            }

            Cell.visited.Add(cell);
            size++;
            size = Move(row, column + 1, size);
            size = Move(row, column - 1, size);
            size = Move(row - 1, column, size);
            size = Move(row + 1, column, size);

            return size;
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

            public bool IsValid()
            {
                if (this.Row >= 0 && this.Row < matrix.GetLength(0) &&
                    this.Column >= 0 && this.Column < matrix.GetLength(1))
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

        public class Area : IComparable<Area>
        {
            public Area(Program.Cell position)
            {
                this.Position = position;
            }

            public int Size { get; set; }

            public Program.Cell Position { get; set; }

            public int CompareTo(Area other)
            {
                if (this.Size.Equals(other.Size))
                {
                    if (this.Position.Row.Equals(other.Position.Row))
                    {
                        return this.Position.Column.CompareTo(other.Position.Column);
                    }

                    return this.Position.Row.CompareTo(other.Position.Row);
                }

                return other.Size.CompareTo(this.Size);
            }
        }
    }
}
