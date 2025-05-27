namespace DSS.Lab3.Demo;

public sealed class Alternative
{
    public string Name { get; }
    public decimal[] Features { get; }
    
    public Alternative(string name, decimal[] features)
    {
        ArgumentNullException.ThrowIfNull(name);
        Name = name;
        ArgumentNullException.ThrowIfNull(features);
        if (features.Length == 0)
        {
            throw new ArgumentException("Features array must not be empty", nameof(features));       
        }
        Features = features;
    }
    
    public bool IsDominatedBy(Alternative anotherAlternative, Direction[] directions)
    {
        ArgumentNullException.ThrowIfNull(anotherAlternative);
        ArgumentNullException.ThrowIfNull(directions);
        
        bool isWorseByAtLeastOneFeature = false;

        for (var i = 0; i < Features.Length; i++)
        {
            if ((directions[i] == Direction.Direct && Features[i] > anotherAlternative.Features[i])
                || (directions[i] == Direction.Reverse && Features[i] < anotherAlternative.Features[i]))
            {
                return false;
            }

            if ((directions[i] == Direction.Direct && Features[i] < anotherAlternative.Features[i])
                || (directions[i] == Direction.Reverse && Features[i] > anotherAlternative.Features[i]))
            {
                isWorseByAtLeastOneFeature = true;
            }
        }
        
        return isWorseByAtLeastOneFeature;
    }
}
