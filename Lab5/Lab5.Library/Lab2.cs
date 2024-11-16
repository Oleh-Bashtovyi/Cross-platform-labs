using Lab_2;

namespace LabLibrary;

public static class Lab2
{
    public static string Run(string inputFile)
    {
        var productBlocks = IOHandler.ReadProductBlocks(inputFile);
        var result = BlocksCombiningProblemSolver.Solve(productBlocks.ToArray());
        return result.ToString();
    }
}
