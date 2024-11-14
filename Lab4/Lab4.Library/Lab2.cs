using Lab_2;

namespace LabLibrary;

public class Lab2
{
    public static void Run(string inputFile, string outputFile)
    {
        Console.WriteLine("LAB #2");
        Console.WriteLine("Input data:");
        var productBlocks = IOHandler.ReadProductBlocks(inputFile);
        Console.WriteLine(string.Join(Environment.NewLine, productBlocks).Trim());
        Console.WriteLine("Output data:");
        var result = BlocksCombiningProblemSolver.Solve(productBlocks.ToArray());
        Console.WriteLine(result);
        IOHandler.WriteResult(result, outputFile);
        Console.WriteLine($"Result successfuly written to: {outputFile}");
    }
}