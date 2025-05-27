using System.Globalization;
using System.Text;
using DSS.Lab3.Demo;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;
CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("uk-UA");

var alternatives = new List<Alternative>
{
    new Alternative("A1", [1, 1, 2, 3]),
    new Alternative("A2", [1, 2, 3, 3]),
    new Alternative("A3", [2, 3, 2, 1]),
    new Alternative("A4", [1, 2, 2, 4]),
    new Alternative("A5", [1, 3, 1, 2]),
    new Alternative("A6", [2, 1, 2, 1]),
    new Alternative("A7", [2, 2, 2, 3]),
    new Alternative("A8", [2, 2, 2, 1]),
    new Alternative("A9", [1, 3, 1, 3]),
    new Alternative("A10", [2, 2, 2, 4])
};
var weights = new[] { 0.35m, 0.2m, 0.3m, 0.15m };
var directions = new[] { Direction.Reverse, Direction.Direct, Direction.Direct, Direction.Reverse };

var service = new EdgeworthParetoService();
var printer = new EdgeworthParetoPrinter(service);

printer.PrintCurrentValues(alternatives, weights, directions);
var efficientSet = printer.GetEfficientSet(alternatives, directions);
var efficientSetWithWeights = printer.GetEfficientSetWithWeights(efficientSet, weights);
var normalizedEfficientSet = printer.GetNormalizedEfficientSet(efficientSetWithWeights);
_ = printer.GetResults(normalizedEfficientSet, directions);
