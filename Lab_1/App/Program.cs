namespace App;

internal static class Program
{
    private static void Main() 
    { 
        try
        {
            var orders = IOHandler.ReadOrders();

            var result = OrdersProblemSolver.Solve(orders);

            IOHandler.WriteResult(result);

            Console.WriteLine("Result successfuly written to file!");
        }
        catch (Exception ex)
        {
            var prevColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
            Console.ForegroundColor = prevColor;
        }
        Console.WriteLine("Program finished!");
    }
}
