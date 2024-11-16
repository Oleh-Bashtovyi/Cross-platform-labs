using Lab_3;
using LabLibrary.Extensions;

namespace LabLibrary;


public static class Lab3
{
    public static string Run(string inputFile)
    {
        var matrix = IOHandler.ReadMatrixFromFile(inputFile);
        var result = Solution.Solve(matrix);
        return matrix.ToFormattedString();
    }
}

