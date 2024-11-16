namespace Lab_3;

public class SquareMatrixValidator
{
    public readonly int MinMatrixSize;
    public readonly int MaxMatrixSize;
    public readonly int MinMatrixValues;
    public readonly int MaxMatrixValues;

    public SquareMatrixValidator(int minMatrixSize, int maxMatrixSize, int minMatrixValues, int maxMatrixValues)
    {
        MinMatrixSize = minMatrixSize;
        MaxMatrixSize = maxMatrixSize;
        MinMatrixValues = minMatrixValues;
        MaxMatrixValues = maxMatrixValues;
    }

    public void Validate(int[,] matrix)
    {
        if (matrix == null)
        {
            throw new ArgumentNullException(nameof(matrix), "Matrix cannot be null.");
        }

        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        if (rows != cols)
        {
            throw new ArgumentException("Matrix must be square.");
        }

        if (rows < MinMatrixSize || rows > MaxMatrixSize)
        {
            throw new ArgumentOutOfRangeException(nameof(matrix), $"Matrix dimensions must be between {MinMatrixSize} and {MaxMatrixSize}.");
        }

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                int value = matrix[i, j];
                if (value < MinMatrixValues || value > MaxMatrixValues)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(matrix),
                        $"Matrix elements must be between {MinMatrixValues} and {MaxMatrixValues}.");
                }
            }
        }
    }
}

