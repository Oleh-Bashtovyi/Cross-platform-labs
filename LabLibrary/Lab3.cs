using Lab_3;

namespace LabLibrary;

public class Lab3 : LabBase
{
    public override void Run(string inputFile, string outputFile, bool enableLog = false)
    {
        var matrix = IOHandler.ReadMatrixFromFile(inputFile);
        var result = Solution.Solve(matrix);
        IOHandler.WriteMatrixToFile(result, outputFile);

        if (enableLog)
        {
            Console.WriteLine("LAB #3");
            Console.WriteLine("Input data:");
            PrintMatrix(matrix);
            Console.WriteLine("Output data:");
            PrintMatrix(result);
            Console.WriteLine($"Result successfuly written to: {outputFile}");
        }
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