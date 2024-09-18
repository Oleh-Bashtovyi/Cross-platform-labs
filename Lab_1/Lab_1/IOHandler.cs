namespace App;

public static class IOHandler
{
    private const string InputFileName = "INPUT.TXT";
    private const string OutputFileName = "OUTPUT.TXT";


    public static List<Order> ReadOrders()
    {
        return ReadOrders(InputFileName);
    }

    public static List<Order> ReadOrders(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("Input file not found.");
        }

        var lines = File.ReadAllLines(filePath)
              .Where(static line => !string.IsNullOrWhiteSpace(line))
              .ToList();

        if (lines.Count == 0)
        {
            throw new FormatException("File is empty");
        }


        if (!int.TryParse(lines[0], out int numberOfOrders))
        {
            throw new FormatException($"Unable to parse value: {lines[0]}.");
        }

        var orders = new List<Order>();

        for (int i = 1; i <= numberOfOrders; i++)
        {
            if (i >= lines.Count)
            {
                throw new FormatException($"File dont have specified amount of orders. Expected: {numberOfOrders}, Actual: {lines.Count - 1}");
            }

            var parts = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 2)
            {
                throw new FormatException($"Row {i + 1}: must be exact 2 numbers \"deadline reward\"");
            }

            if (!int.TryParse(parts[0], out int deadline))
            {
                throw new FormatException($"Row {i + 1}: unable to convert deadline: {parts[0]} ");
            }

            if (!int.TryParse(parts[1], out int reward))
            {
                throw new FormatException($"Row {i + 1}: unable to convert reward: {parts[1]} ");
            }

            orders.Add(new(deadline, reward));
        }

        return orders;
    }

    public static void WriteResult(int result)
    {
        WriteResult(result, OutputFileName);
    }

    public static void WriteResult(int result, string filePath)
    {
        File.WriteAllText(OutputFileName, result.ToString());
    }
}
