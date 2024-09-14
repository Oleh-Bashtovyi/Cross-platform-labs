namespace Lab_1;

public static class OrdersReader
{
    public static List<Order> ReadOrders(string filePath)
    {
        var lines = File.ReadAllLines(filePath);
        
        if (lines.Length == 0)
        {
            throw new FormatException("File is empty");
        }


        if (!int.TryParse(lines[0], out int numberOfOrders))
        {
            throw new FormatException($"Row 0: unable to parse value: {lines[0]}.");
        }

        var orders = new List<Order>();

        for (int i = 1; i <= numberOfOrders; i++)
        {
            if (i >= lines.Length)
            {
                throw new FormatException($"File dont have specified amount of orders. Expected: {numberOfOrders}, Actual: {lines.Length - 1}");
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
}
