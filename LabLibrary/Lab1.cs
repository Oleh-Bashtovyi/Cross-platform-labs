using Lab_1;

namespace LabLibrary;

public class Lab1 : LabBase
{
    public override void Run(string inputFile, string outputFile, bool enableLog = false)
    {

        var orders = IOHandler.ReadOrders(inputFile);
        var result = OrdersProblemSolver.Solve(orders);
        IOHandler.WriteResult(result, outputFile);

        if (enableLog)
        {
            Console.WriteLine("LAB #1");
            Console.WriteLine("Input data:");
            Console.WriteLine(string.Join(Environment.NewLine, orders).Trim());
            Console.WriteLine("Output data:");
            Console.WriteLine(result);
            Console.WriteLine($"Result successfuly written to: {outputFile}");
        }
    }
}