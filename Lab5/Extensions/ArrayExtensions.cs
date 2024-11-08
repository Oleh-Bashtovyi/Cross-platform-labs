namespace Lab5.Extensions;

public static class ArrayExtensions
{
    public static string ToFormattedString(this int[,] array)
    {
        int rows = array.GetLength(0);
        int cols = array.GetLength(1);
        var result = new List<string>();

        for (int i = 0; i < rows; i++)
        {
            var row = new List<string>();
            for (int j = 0; j < cols; j++)
            {
                row.Add(array[i, j].ToString());
            }
            result.Add(string.Join(" ", row));
        }

        return string.Join("\n", result);
    }
}
