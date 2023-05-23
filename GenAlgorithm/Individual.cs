using SolutionItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenAlgorithm
{
    public class Individual
    {
        public int XFenotype { get; set; }
        public double ResultMainFunc { get; set; }
        public Hromosome20Bit Hromosome { get; set; }
        public double FitnessIndividual { get; set; }

        public Individual(int x)
        {
            ResultMainFunc = Consts.func(x);
            Hromosome = new Hromosome20Bit(x);
            XFenotype = x;
            FitnessIndividual = 1;
        }

        public Individual(Hromosome20Bit hrom)
        {
            XFenotype = hrom.GetInt();
            ResultMainFunc = Consts.func(XFenotype);
            FitnessIndividual = 1;
        }
    }
}
