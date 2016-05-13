using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_QueensPuzzle
{
    class Program
    {
        static void Main(string[] args)
        {
            EightQueens.PutQueen(0);
        }
    }

    class EightQueens
    {
        const int Size = 8;
        static bool[,] chessboard = new bool[Size, Size];
        static int solutionFound = 0;
        static bool[] attackedColumns = new bool[8];
        static bool[] attackedLeftDiagonals = new bool[15];
        static bool[] attackedRightDiagonals = new bool[15];
         
        public static void PutQueen(int row)
        {
            if (row == Size)
            {
                PrintSolution();
            }
            else
            {
                for (int col = 0; col < Size; col++)
                {
                    if (CanPlaceQueen(row, col))
                    {
                        MarkAllAttackedPositions(row, col);
                        PutQueen(row + 1);
                        UnmarkAllAttackedPositions(row, col);
                    }
                }
            }
        }

        private static void PrintSolution()
        {
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    if (chessboard[row, col])
                    {
                        Console.Write('*');
                    }
                    else
                    {
                        Console.Write('-');
                    }
                }
                Console.WriteLine();
            }

            solutionFound++;
            Console.WriteLine(solutionFound);
            Console.WriteLine();
        }

        private static void UnmarkAllAttackedPositions(int row, int col)
        {
            attackedColumns[col] = false;
            attackedLeftDiagonals[col - row + 7] = false;
            attackedRightDiagonals[row + col] = false;
            chessboard[row, col] = false;
        }

        private static void MarkAllAttackedPositions(int row, int col)
        {
            attackedColumns[col] = true;
            attackedLeftDiagonals[col - row + 7] = true;
            attackedRightDiagonals[row + col] = true;
            chessboard[row, col] = true;
        }

        private static bool CanPlaceQueen(int row, int col)
        {
            var positionOccuried = 
                attackedColumns[col] ||
                attackedLeftDiagonals[col - row + 7] || 
                attackedRightDiagonals[col + row];

            return !positionOccuried;
        }
    }
}
