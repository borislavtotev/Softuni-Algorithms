using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Parenthesis
{
    class Program
    {
        static int n;

        static void Main(string[] args)
        {
            n = int.Parse(Console.ReadLine());
            var array = new int[2*n];
            GeneratePermutations(new Stack<string>(), 0, new Stack<int>());
        }

        static void GeneratePermutations(Stack<string> list, int index, Stack<int> stack)
        {
            if (list.Count == 2 * n && index >= list.Count - 1 && stack.Count == 0)
            {
                Console.WriteLine(string.Join("", list.Reverse()));
                return;
            }

            if (stack.Count < n &&  stack.Count + 1 <= 2 * n - index - 1)
            {
                list.Push("(");
                stack.Push(0);
                GeneratePermutations(list, index + 1, stack);
                stack.Pop();
                list.Pop();
            }

            if (stack.Count > 0)
            {
                list.Push(")");
                stack.Pop();
                GeneratePermutations(list, index + 1, stack);
                stack.Push(0);
                list.Pop();
            }
        }
    }
}
