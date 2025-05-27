namespace DSS.Lab3.Demo;

public sealed class EdgeworthParetoPrinter(IEdgeworthParetoService service)
{
    public void PrintCurrentValues(IEnumerable<Alternative> alternatives, decimal[] weights, Direction[] directions)
    {
        Console.WriteLine("Альтернативи:");
        PrintAlternatives(alternatives);
        Console.WriteLine("Ваги:");
        Console.WriteLine($"{{ {string.Join(", ", weights)} }}");
        Console.WriteLine("Напрями:");
        Console.WriteLine($"{{ {string.Join(", ", directions.Select(d => d == Direction.Direct ? "Прямий" : "Зворотній"))} }}");
        Console.WriteLine();
    }
    
    public IReadOnlyList<Alternative> GetEfficientSet(IReadOnlyList<Alternative> alternatives, Direction[] directions)
    {
        Console.WriteLine("Ефективна множина рішень: ");
        
        var efficientSet = service.GetEfficientSet(alternatives, directions);

        PrintAlternatives(efficientSet);

        Console.WriteLine();
        
        return efficientSet;
    }

    public IReadOnlyList<Alternative> GetEfficientSetWithWeights(IReadOnlyList<Alternative> efficientSet, decimal[] weights)
    {
        Console.WriteLine("Ефективна множина рішень з врахуванням значень вагових коефіцієнтів: ");
        
        var efficientSetWithWeights = service.GetEfficientSetWithWeights(efficientSet, weights);

        PrintAlternatives(efficientSetWithWeights);

        Console.WriteLine();
        
        return efficientSetWithWeights;
    }

    public IReadOnlyList<Alternative> GetNormalizedEfficientSet(IReadOnlyList<Alternative> efficientSetWithWeights)
    {
        Console.WriteLine("Нормовані значення ефективної множини рішень: ");
        
        var normalizedEfficientSet = service.GetNormalizedEfficientSet(efficientSetWithWeights);

        PrintAlternatives(normalizedEfficientSet);

        Console.WriteLine();
        
        return normalizedEfficientSet;
    }

    public IReadOnlyList<ResultAlternative> GetResults(IReadOnlyList<Alternative> normalizedEfficientSet, Direction[] directions)
    {
        Console.WriteLine("Результати: ");
        var results = service.GetResults(normalizedEfficientSet, directions);
        foreach (var result in results)
        {
            Console.WriteLine($"F {result.Name} = {result.FScore:0.##}");
        }

        if (results.Count > 0)
        {
            var optimalAlternatives = results
                .OrderByDescending(r => r.FScore)
                .ToList();
            var bestFScore = optimalAlternatives[0].FScore;
            optimalAlternatives = optimalAlternatives
                .Where(a => a.FScore == bestFScore)
                .OrderBy(a => a.Name)
                .ToList();

            if (optimalAlternatives.Count == 1)
            {
                Console.WriteLine($"Оптимальною є альтернатива {optimalAlternatives[0].Name}.");
            }
            else
            {
                Console.WriteLine($"Оптимальними є альтернативи {string.Join(", ", optimalAlternatives.Select(a => a.Name))}.");
            }
        }
        
        Console.WriteLine();

        return results;
    }

    private static void PrintAlternatives(IEnumerable<Alternative> alternatives)
    {
        foreach (var alternative in alternatives)
        {
            Console.Write($"{alternative.Name,-8}");

            foreach (var feature in alternative.Features)
            {
                Console.Write($"{feature,-8:0.##}");
            }
            
            Console.WriteLine();
        }
    }
}
