using Lab_3;

namespace LabLibrary;

public class Lab3
{
    public static void Run(string inputFile, string outputFile)
    {
        Console.WriteLine("LAB #3");
        Console.WriteLine("Input data:");
        var matrix = IOHandler.ReadMatrixFromFile(inputFile);
        PrintMatrix(matrix);
        Console.WriteLine("Output data:");
        var result = Solution.Solve(matrix);
        PrintMatrix(result);
        IOHandler.WriteMatrixToFile(result, outputFile);
        Console.WriteLine($"Result successfuly written to: {outputFile}");
    }

    private static void PrintMatrix(int[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write($"{matrix[i, j],4} "); // Вирівнювання по ширині 4
            }
            Console.WriteLine();
        }
    }
}