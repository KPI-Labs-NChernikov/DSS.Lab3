namespace DSS.Lab3.Demo;

public sealed class EdgeworthParetoService : IEdgeworthParetoService
{
    public IReadOnlyList<Alternative> GetEfficientSet(IReadOnlyList<Alternative> alternatives, Direction[] directions)
    {
        var alternativesList = alternatives.ToList();
        var efficientSet = new List<Alternative>();

        foreach (var alternative in alternativesList)
        {
            var isDominated = false;
            
            foreach (var anotherAlternative in alternativesList.Where(a => alternative != a))
            {
                isDominated = alternative.IsDominatedBy(anotherAlternative, directions);
                if (isDominated)
                {
                    break;
                }
            }
            
            if (!isDominated)
            {
                efficientSet.Add(alternative);           
            }
        }

        return efficientSet;
    }

    public IReadOnlyList<Alternative> GetEfficientSetWithWeights(IReadOnlyList<Alternative> efficientSet, decimal[] weights)
    {
        var efficientSetWithWeights = new Alternative[efficientSet.Count];

        for (var i = 0; i < efficientSet.Count; i++)
        {
            var features = new decimal[efficientSet[i].Features.Length];
            for (var j = 0; j < efficientSet[i].Features.Length; j++)
            {
                features[j] = efficientSet[i].Features[j] * weights[j];           
            }
            
            efficientSetWithWeights[i] = new Alternative(efficientSet[i].Name, features);           
        }

        return efficientSetWithWeights;
    }

    public IReadOnlyList<Alternative> GetNormalizedEfficientSet(IReadOnlyList<Alternative> efficientSetWithWeights)
    {
        var normalizedEfficientSet = new Alternative[efficientSetWithWeights.Count];
        
        var maxFeatures = GetMaxFeatures(efficientSetWithWeights);
        
        for (var i = 0; i < efficientSetWithWeights.Count; i++)
        {
            var features = new decimal[efficientSetWithWeights[i].Features.Length];
            for (var j = 0; j < efficientSetWithWeights[i].Features.Length; j++)
            {
                if (maxFeatures[j] != 0)
                {
                    features[j] = efficientSetWithWeights[i].Features[j] / maxFeatures[j];
                }
            }
            
            normalizedEfficientSet[i] = new Alternative(efficientSetWithWeights[i].Name, features);           
        }
        
        return normalizedEfficientSet;       
    }

    private decimal[] GetMaxFeatures(IReadOnlyList<Alternative> alternatives)
    {
        var maxFeatures = new decimal[alternatives[0].Features.Length];

        for (var i = 0; i < alternatives[0].Features.Length; i++)
        {
            maxFeatures[i] = alternatives.Max(a => a.Features[i]);           
        }
        
        return maxFeatures;
    }

    public IReadOnlyList<ResultAlternative> GetResults(IReadOnlyList<Alternative> normalizedEfficientSet, Direction[] directions)
    {
        var results = new ResultAlternative[normalizedEfficientSet.Count];

        for (var i = 0; i < normalizedEfficientSet.Count; i++)
        {
            var sum = 0m;

            for (var j = 0; j < normalizedEfficientSet[i].Features.Length; j++)
            {
                sum += directions[j] == Direction.Direct 
                    ? normalizedEfficientSet[i].Features[j] 
                    : -normalizedEfficientSet[i].Features[j];
            }
            
            results[i] = new ResultAlternative(normalizedEfficientSet[i].Name, sum);           
        }
        
        return results;       
    }
}
