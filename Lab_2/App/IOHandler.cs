namespace App;

public static class IOHandler
{
    private const string InputFileName = "INPUT.TXT";
    private const string OutputFileName = "OUTPUT.TXT";


    public static List<ProductBlock> ReadProductBlocks()
    {
        return ReadProductBlocks(InputFileName);
    }

    public static List<ProductBlock> ReadProductBlocks(string filePath)
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


        if (!int.TryParse(lines[0], out int numberOfBlocks))
        {
            throw new FormatException($"Unable to parse value: {lines[0]}.");
        }

        var blocks = new List<ProductBlock>();







        for (int i = 1; i <= numberOfBlocks; i++)
        {
            if (i >= lines.Count)
            {
                throw new FormatException($"File dont have specified amount of product blocks. Expected: {numberOfBlocks}, Actual: {lines.Count - 1}");
            }

            var parts = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 2)
            {
                throw new FormatException($"Row {i + 1}: must be exact 2 numbers \"mi ki\"");
            }

            if (!int.TryParse(parts[0], out int deadline))
            {
                throw new FormatException($"Row {i + 1}: unable to convert mi: {parts[0]} ");
            }

            if (!int.TryParse(parts[1], out int reward))
            {
                throw new FormatException($"Row {i + 1}: unable to convert ki: {parts[1]} ");
            }

            blocks.Add(new(deadline, reward));
        }

        return blocks;
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
