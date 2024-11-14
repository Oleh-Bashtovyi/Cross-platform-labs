using Lab_1;

namespace LabLibrary;

public class Lab1
{
    public static void Run(string inputFile, string outputFile)
    {
        Console.WriteLine("LAB #1");
        Console.WriteLine("Input data:");
        var orders = IOHandler.ReadOrders(inputFile);
        Console.WriteLine(string.Join(Environment.NewLine, orders).Trim());
        Console.WriteLine("Output data:");
        var result = OrdersProblemSolver.Solve(orders);
        Console.WriteLine(result);
        IOHandler.WriteResult(result, outputFile);
        Console.WriteLine($"Result successfuly written to: {outputFile}");
    }
}