/*
 Вариант 10.
 Целевая функция: y=0,3*x+x*cos(7,3*x)-0,7*sin(1,3*x) 
 Экстремум: max
 Способ кодирования хромосом: код Грея
 Вероятности: 
    Pc(скрещивания)=0.8
    Pm(мутации)=0.2
 Генетические операторы:
    Формирование родительских пар - случайный отбор
    редукция - рулетка
 Размер популяции - 50 особей
 Длина хромосомы - 20 бит
 Область поиска [-5;5]
 */


using GenAlgorithm;
using SolutionItems;
int numberPopulation = 1;
Random roulette = new Random();
List<Individual> individuals;
Dictionary<double, int> rouletteDict = new Dictionary<double, int>();
GenerateFirstIndividuals();
GenerateNewPopulation();
while(individuals.Any(x => x.FitnessIndividual > 0.1) || individuals.Count == 1)
{
    List<Individual> newIndividuals = new List<Individual>();
    double SumResultFunc = individuals.Sum(x => x.ResultMainFunc);
    foreach(var el in individuals)
    {
        el.FitnessIndividual = el.ResultMainFunc / SumResultFunc;
    }
    Console.WriteLine($"{numberPopulation}: Средняя приспособленность особей = {individuals.Sum(x => x.FitnessIndividual) / individuals.Count}\n");
    while(newIndividuals.Count < Consts.SizePopulation) //roulette
    {
        CreateRoulette();
        var rnd = roulette.NextDouble();
        if (!rouletteDict.Any()) break;
        var temp = rouletteDict.Where(x => x.Key < rnd)?.Max(x => x.Key);
        if (temp != null)
        {
            rouletteDict.TryGetValue(temp.Value, out int indexOfIndividual);
            newIndividuals.Add(individuals[indexOfIndividual+1]);
            individuals.Remove(individuals[indexOfIndividual+1]);
        }
    }
    individuals = newIndividuals;
    numberPopulation++;
    if (individuals.FirstOrDefault(x => x.FitnessIndividual < 0.1) != null)
        Console.WriteLine(individuals.First(x => x.FitnessIndividual < 0.1));
    else Console.WriteLine(individuals.First());
}

void CreateRoulette()
{
    double rouletteBegin = 0;
    foreach (var el in individuals)
    {
        var obj = rouletteDict.TryGetValue(rouletteBegin += el.FitnessIndividual, out int value);
        if(!obj) rouletteDict.Add(rouletteBegin, individuals.IndexOf(el));
    }
}


void GenerateFirstIndividuals()
{
    individuals = new List<Individual>(Consts.SizePopulation);
    Random rnd = new Random();
    for(int i=0; i<Consts.SizePopulation; i++)
    {
        int value = rnd.Next(Consts.BottomLimit, Consts.UpLimit);
        individuals.Add(new Individual(value));
    }
}

void GenerateNewPopulation()
{
    Random rnd = new Random();
    List<Individual> newPopulation = new List<Individual>();
    foreach(var el in individuals)
    {
        int crossIndividual = rnd.Next(0, individuals.Count - 1);
        int fromBit = rnd.Next(0, 19);
        double crossOrMutate = rnd.NextDouble();
        if (crossOrMutate > 0 && crossOrMutate < Consts.ProbabilityCrossing)
        {
            newPopulation.Add(new Individual(el.Hromosome.GetFirstSon(individuals[crossIndividual].Hromosome, fromBit)));
            newPopulation.Add(new Individual(el.Hromosome.GetSecondSon(individuals[crossIndividual].Hromosome, fromBit)));
        }
        else
        {
            el.Hromosome.mutation();
            newPopulation.Add(el);
        }
    }
    individuals = newPopulation.GroupBy(p => p.ResultMainFunc).Select(g => g.First()).ToList();
}