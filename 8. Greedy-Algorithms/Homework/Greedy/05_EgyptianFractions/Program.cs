using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05_EgyptianFractions
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputNumbers = Console.ReadLine().Split('/').Select(decimal.Parse).ToList();
            var nominator = inputNumbers[0];
            var denominator = inputNumbers[1];

            if (nominator >= denominator)
            {
                Console.WriteLine("Error (fraction is equal to or greater than 1)");
                return;
            }

            if (nominator == 1)
            {
                Console.WriteLine("{0}/{1} = {0}/{1}", nominator, denominator, nominator, denominator);
                return;
            }

            var currentDenominator = 2;
            var resultDenominators = new List<string>();
            var baseNominator = nominator;
            var baseDenominator = denominator;

            while (baseNominator > 0)
            {
                var newBaseNominator = baseNominator * currentDenominator - baseDenominator;
                var newBaseDenominator = baseDenominator * currentDenominator;
                if (newBaseNominator < baseNominator && newBaseNominator >= 0) 
                {
                    resultDenominators.Add(string.Format("1/{0}", currentDenominator));
                    baseNominator = newBaseNominator;
                    baseDenominator = newBaseDenominator;
                }
                    
                currentDenominator++;
            }

            Console.WriteLine("{0}/{1} = {2}", nominator, denominator, string.Join("+", resultDenominators));
        }
    }
}
