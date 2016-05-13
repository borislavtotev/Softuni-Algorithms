using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04_Salaries
{
    class Program
    {
        static bool[,] peopleMaxrix;
        static decimal[] peopleSalaries;
         
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            peopleMaxrix = new bool[n, n];
            peopleSalaries = new decimal[n];
            for (int i = 0; i < n; i++)
            {
                var inputElements = Console.ReadLine().ToArray();
                for (int j = 0; j < inputElements.Length; j++)
                {
                    if (inputElements[j] == 'Y')
                    {
                        peopleMaxrix[i, j] = true;
                    }
                }
            }

            decimal sum = 0;
            for (int personIndex = 0; personIndex < peopleMaxrix.GetLength(0); personIndex++)
            {
                CalculateSalary(personIndex);

                sum += peopleSalaries[personIndex];
            }

            Console.WriteLine(sum);
        }

        static void CalculateSalary(int person)
        {
            if (peopleSalaries[person] == 0)
            {
                bool manager = false;
                for (int i = 0; i < peopleMaxrix.GetLength(1); i++)
                {
                    if (peopleMaxrix[person, i])
                    {
                        manager = true;
                        CalculateSalary(i);
                        peopleSalaries[person] += peopleSalaries[i];
                    }
                }

                if (!manager)
                {
                    peopleSalaries[person] = 1;
                }
            }
        }
    }
}
