namespace App;


public static class IOHandler
{
    public const int MAX_MATRIX_SIZE = 200;

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
                throw new InvalidOperationException("File is empty.");
            }

            if (!int.TryParse(lines[0], out int matrixDimension) || matrixDimension <= 0)
            {
                throw new InvalidOperationException($"{lines[0]} - Invalid matrix dimension.");
            }

            if (matrixDimension > MAX_MATRIX_SIZE)
            {
                throw new InvalidOperationException($"Matrix size should be less or equal: {MAX_MATRIX_SIZE}");
            }

            if (lines.Length - 1 != matrixDimension)
            {
                throw new InvalidOperationException($"Matrix must have {matrixDimension} rows.");
            }

            var matrix = new int[matrixDimension, matrixDimension];

            for (int i = 0; i < matrixDimension; i++)
            {
                var row = lines[i + 1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (row.Length != matrixDimension)
                {
                    throw new InvalidOperationException($"Matrix row {i + 1} must have {matrixDimension} elements.");
                }

                for (int j = 0; j < matrixDimension; j++)
                {
                    if (!int.TryParse(row[j], out matrix[i, j]))
                    {
                        throw new InvalidOperationException($"Invalid value at row {i + 1}, column {j + 1}. Actual value: {row[j]}");
                    }
                }
            }

            return matrix;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Error reading matrix from file: {ex.Message}");
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
            throw new InvalidOperationException($"Error writing matrix to file: {ex.Message}");
        }
    }
}

