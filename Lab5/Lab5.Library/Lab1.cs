using Lab_1;

namespace LabLibrary;

public static class Lab1
{
    public static string Run(string inputFile)
    {
        var orders = IOHandler.ReadOrders(inputFile);
        var result = OrdersProblemSolver.Solve(orders);
        return result.ToString();
    }
}
