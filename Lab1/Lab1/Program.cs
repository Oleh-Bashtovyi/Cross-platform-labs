namespace Lab_1;

internal static class Program
{
    public const string INPUT_FILENAME = "INPUT.TXT";
    public const string OUTPUT_FILENAME = "OUTPUT.TXT";

    private static void Main() 
    {
        var prevConsoleColor = Console.ForegroundColor;
        try
        {
            var orders = IOHandler.ReadOrders(INPUT_FILENAME);

            var result = OrdersProblemSolver.Solve(orders);

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
