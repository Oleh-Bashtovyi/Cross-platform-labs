namespace App;

internal static class Program
{
    public const string INPUT_FILENAME = "INPUT.TXT";
    public const string OUTPUT_FILENAME = "OUTPUT.TXT";

    private static void Main()
    {
        var prevConsoleColor = Console.ForegroundColor;
        try
        {
            var blocks = IOHandler.ReadProductBlocks(INPUT_FILENAME);

            var result = BlocksCombiningProblemSolver.Solve(blocks.ToArray());

            IOHandler.WriteResult(result, OUTPUT_FILENAME);

            Console.WriteLine("Result successfuly written to file!");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
        }
        finally
        {
            Console.ForegroundColor = prevConsoleColor;
        }
        Console.WriteLine("Program finished!");
    }
}
