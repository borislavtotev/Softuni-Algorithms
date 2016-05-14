using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _04_NestedRectangles
{
    class Program
    {
        static void Main(string[] args)
        {
            var rectangles = new SortedDictionary<string, Rectangle>();

            var input = Console.ReadLine();
            while (input != "End")
            {
                var elements = Regex.Split(input, "[^\\d\\-]+");
                var nameString = input.Split(':')[0];
                rectangles.Add(nameString, new Rectangle(
                    nameString, int.Parse(elements[1]),
                    int.Parse(elements[2]), int.Parse(elements[3]), int.Parse(elements[4])));
                input = Console.ReadLine();
            }

            Console.WriteLine();
        }
    }

    class Rectangle : IComparable<Rectangle>
    {
        public Rectangle(string name, int left, int top, int right, int bottom)
        {
            this.Name = name;
            this.Left = left;
            this.Top = top;
            this.Right = right;
            this.Bottom = bottom;
        }

        public int Bottom { get; private set; }
        public int Left { get; private set; }
        public string Name { get; private set; }
        public int Right { get; private set; }
        public int Top { get; private set; }

        public int CompareTo(Rectangle other)
        {
            return this.Name.CompareTo(other.Name);
        }
    }
}
