using System.Reflection;
using Days;

var solvers = Assembly.GetExecutingAssembly().GetTypes()
    .Where(t => typeof(ISolution).IsAssignableFrom(t)
                && t is { IsInterface: false, IsAbstract: false })
    .OrderBy(t => t.Namespace)
    .ToList();

Console.WriteLine($"Encontrados {solvers.Count} dias.");

foreach (var solverType in solvers)
{
    var solver = (ISolution)Activator.CreateInstance(solverType)!;
    
    Console.WriteLine($"Rodando: {solverType.Namespace}");
    Console.WriteLine($"P1: {solver.RunPart1()}");
    Console.WriteLine($"P2: {solver.RunPart2()}");
    Console.WriteLine();
}