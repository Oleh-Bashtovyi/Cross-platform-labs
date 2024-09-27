namespace App;

public static class IOHandler
{

    public const int MIN_BLOCK_COUNT = 1;
    public const int MAX_BLOCK_COUNT = 100;


    public static List<ProductBlock> ReadOrders(string filePath)
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
            throw new FormatException(
                $"Unable to parse first line (number of blocks): {lines[0]}.");
        }

        if (numberOfOrders < MIN_BLOCK_COUNT || numberOfOrders > MAX_BLOCK_COUNT)
        {
            throw new ArgumentOutOfRangeException(
                nameof(numberOfOrders),
                $"Number of blocks (first line) should be between {MIN_BLOCK_COUNT} and {MAX_BLOCK_COUNT}{Environment.NewLine}" +
                $"Actual value: {numberOfOrders}");
        }

        if (numberOfOrders >= lines.Count)
        {
            throw new FormatException(
                $"File dont have specified number of product blocks.{Environment.NewLine}" +
                $"Expected: {numberOfOrders}, Actual: {lines.Count - 1}");
        }

        var orders = new List<ProductBlock>();

        for (int i = 1; i <= numberOfOrders; i++)
        {
            var parts = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 2)
            {
                throw new FormatException($"Row {i + 1}: must be exact 2 numbers \"left_part right_part\"");
            }

            if (!int.TryParse(parts[0], out int deadline))
            {
                throw new FormatException($"Row {i + 1}: unable to convert left part: {parts[0]} ");
            }

            if (!int.TryParse(parts[1], out int reward))
            {
                throw new FormatException($"Row {i + 1}: unable to convert right part: {parts[1]} ");
            }

            orders.Add(new(deadline, reward));
        }

        return orders;
    }


    public static void WriteResult(int result, string filePath)
    {
        File.WriteAllText(filePath, result.ToString());
    }
}
