namespace Lab_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var outputFile = "output.txt";

            string? inputFilePath;

            if (args.Length > 0)
            {
                inputFilePath = args[0];
            }
            else
            {
                //relative path
                inputFilePath = "input.txt";
            }

            try
            {
                var orders = OrdersReader.Read(inputFilePath);

                var solution = OrdersProblemSolver.Solve(orders);

                var savePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, outputFile);

                File.WriteAllText(savePath, solution.ToString());

                Console.WriteLine($"Solution was successfuly written to: {savePath}");
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
}
