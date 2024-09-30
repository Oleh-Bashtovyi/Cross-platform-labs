namespace App;


public static class IOHandler
{
    public static int[,] ReadMatrixFromFile(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Input file not found.");
            }

            var lines = File.ReadAllLines(filePath)
                  .Where(static line => !string.IsNullOrWhiteSpace(line))
                  .ToArray();

            if (lines.Length == 0)
            {
                throw new Exception("File is empty.");
            }

            if (!int.TryParse(lines[0], out int matrixDimension) || matrixDimension <= 0)
            {
                throw new Exception($"{lines[0]} - Invalid matrix dimension.");
            }

            if (lines.Length - 1 != matrixDimension)
            {
                throw new Exception($"Matrix must have {matrixDimension} rows.");
            }

            var matrix = new int[matrixDimension, matrixDimension];

            for (int i = 0; i < matrixDimension; i++)
            {
                var row = lines[i + 1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (row.Length != matrixDimension)
                {
                    throw new Exception($"Matrix row {i + 1} must have {matrixDimension} elements.");
                }

                for (int j = 0; j < matrixDimension; j++)
                {
                    if (!int.TryParse(row[j], out matrix[i, j]))
                    {
                        throw new Exception($"Invalid value at row {i + 1}, column {j + 1}. Actual value: {row[j]}");
                    }
                }
            }

            return matrix;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error reading matrix from file: {ex.Message}");
        }
    }


    public static void WriteMatrixToFile(int[,] matrix, string filePath)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                int dimension = matrix.GetLength(0);

                for (int i = 0; i < dimension; i++)
                {
                    for (int j = 0; j < dimension; j++)
                    {
                        writer.Write(matrix[i, j]);

                        if (j < dimension - 1)
                        {
                            writer.Write(" ");
                        }
                    }
                    writer.WriteLine(); 
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error writing matrix to file: {ex.Message}");
        }
    }
}

