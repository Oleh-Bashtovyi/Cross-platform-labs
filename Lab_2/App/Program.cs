namespace App;

internal class Program
{
    static void Main(string[] args)
    {
        try
        {
            var blocks = IOHandler.ReadProductBlocks();

            var result = BlocksCombiningProblemSolver.Solve(blocks.ToArray());

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
