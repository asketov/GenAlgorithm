using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionItems
{
    public static class Consts
    {
        public readonly static int SizePopulation = 50;
        public readonly static double ProbabilityCrossing = 0.8;
        public readonly static double ProbabilityMutation = 0.2;
        public readonly static int BottomLimit = 0;
        public readonly static int UpLimit = 10;
        public static double func(int x)
        {
            return x * x + 0.3 * x + x * Math.Cos(7.3 * x) - 0.7 * Math.Sin(1.3 * x);
        }
    }
}
