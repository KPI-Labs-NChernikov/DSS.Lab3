namespace DSS.Lab3.Demo;

public interface IEdgeworthParetoService
{
    IReadOnlyList<Alternative> GetEfficientSet(IReadOnlyList<Alternative> alternatives, Direction[] directions);

    IReadOnlyList<Alternative> GetEfficientSetWithWeights(IReadOnlyList<Alternative> efficientSet, decimal[] weights);

    IReadOnlyList<Alternative> GetNormalizedEfficientSet(IReadOnlyList<Alternative> efficientSetWithWeights);

    IReadOnlyList<ResultAlternative> GetResults(IReadOnlyList<Alternative> normalizedEfficientSet, Direction[] directions);
}
