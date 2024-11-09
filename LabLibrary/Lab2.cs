using Lab_2;
using System.Text;

namespace LabLibrary;

public class Lab2 : LabBase
{
    public override void Run(string inputFile, string outputFile, bool enableLog = false)
    {
        var productBlocks = IOHandler.ReadProductBlocks(inputFile);
        var result = BlocksCombiningProblemSolver.Solve(productBlocks.ToArray());
        IOHandler.WriteResult(result, outputFile);

        if (enableLog)
        {
            Console.WriteLine("LAB #2");
            Console.WriteLine("Input data:");
            Console.WriteLine(string.Join(Environment.NewLine, productBlocks).Trim());
            Console.WriteLine("Output data:");
            Console.WriteLine(result);
            Console.WriteLine($"Result successfuly written to: {outputFile}");
        }
    }
}