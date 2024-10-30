using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4;

public class LabRunner
{
    public void RunLab1(string inputFile, string outputFile)
    {
        try
        {
            Console.OutputEncoding = Encoding.UTF8;
            var orders = Lab_1.IOHandler.ReadOrders(inputFile);
            var result = Lab_1.OrdersProblemSolver.Solve(orders);
            Lab_1.IOHandler.WriteResult(result, outputFile);
            Console.WriteLine("LAB #1");
            Console.WriteLine("Input data:");
            Console.WriteLine(string.Join(Environment.NewLine, orders).Trim());
            Console.WriteLine("Output data:");
            Console.WriteLine(result);
            Console.WriteLine($"Result successfuly written to: {outputFile}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public void RunLab2(string inputFile, string outputFile)
    {
        try
        {
            Console.OutputEncoding = Encoding.UTF8;
            var productBlocks = Lab_2.IOHandler.ReadProductBlocks(inputFile);
            var result = Lab_2.BlocksCombiningProblemSolver.Solve(productBlocks.ToArray());
            Lab_2.IOHandler.WriteResult(result, outputFile);
            Console.WriteLine("LAB #2");
            Console.WriteLine("Input data:");
            Console.WriteLine(string.Join(Environment.NewLine, productBlocks).Trim());
            Console.WriteLine("Output data:");
            Console.WriteLine(result);
            Console.WriteLine($"Result successfuly written to: {outputFile}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }


    public void RunLab3(string inputFile, string outputFile)
    {
        try
        {
            Console.OutputEncoding = Encoding.UTF8;
            var matrix = Lab_3.IOHandler.ReadMatrixFromFile(inputFile);
            var result = Lab_3.Solution.Solve(matrix);
            Lab_3.IOHandler.WriteMatrixToFile(result, outputFile);
            Console.WriteLine("LAB #3");
            Console.WriteLine("Input data:");
            Console.WriteLine(string.Join(Environment.NewLine, matrix).Trim());
            Console.WriteLine("Output data:");
            Console.WriteLine(string.Join(Environment.NewLine, result).Trim());
            Console.WriteLine($"Result successfuly written to: {outputFile}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
