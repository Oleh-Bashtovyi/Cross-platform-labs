using System.Drawing;

namespace Lab_3;

public static class Solution
{
    public const int MATRIX_MAX_DIMENSION = 200;
    public const int MATRIX_MIN_DIMENSION = 0;
    public const int MATRIX_MAX_VALUES = 1_000_000;
    public const int MATRIX_MIN_VALUES = 0;
    private const int MARKED = -1000000000; //позначає клітину, яку можна відвідали двічі або більше
    private static readonly SquareMatrixValidator s_Validator = new SquareMatrixValidator(MATRIX_MIN_DIMENSION, MATRIX_MAX_DIMENSION, MATRIX_MIN_VALUES, MATRIX_MAX_VALUES);
    private static readonly Point[] Directions =
    [
        new Point(-1, 0), // вгору
        new Point(0, -1), // вліво
        new Point(1, 0),  // вниз
        new Point(0, 1)   // вправо
    ];


    public static int[,] Solve(int[,] matrix)
    {
        s_Validator.Validate(matrix);

        var matrixSize = matrix.GetLength(0);

        if (matrixSize == 0)
        {
            return new int[0, 0];
        }

        if (matrixSize == 1)
        {
            return new int[1, 1] { { matrix[0, 0] } };
        }

        var solution = new int[matrixSize, matrixSize];
        var waves = new int[matrixSize, matrixSize];
        var replace = new int[matrixSize, matrixSize];
        var currentWave = 1;

        var currentQueue = new List<Point>();
        var nextQueue = new List<Point>();

        //Шукаєм ненульові елементи, з яких почнеться поширення хвиль (BFS)
        for (var i = 0; i < matrixSize; i++)
        {
            for (var j = 0; j < matrixSize; j++)
            {
                if(matrix[i, j] != 0)
                {
                    currentQueue.Add(new Point(i, j));
                    replace[i, j] = matrix[i, j];
                    waves[i, j] = currentWave;
                }
            }
        }

        var IsInMatrix = (int x, int y) => !(x < 0 || y < 0 || x >= matrixSize || y >= matrixSize);
        currentWave = 2;

        //BFS відбувається до тих пір, поки не пройдемо усі точки
        while (currentQueue.Count > 0)
        {
            foreach (Point cur in currentQueue)
            {
                foreach (Point dir in Directions)
                {
                    var x = cur.X + dir.X;
                    var y = cur.Y + dir.Y;

                    if (IsInMatrix(x, y))
                    {
                        if (matrix[x, y] == 0)
                        {
                            // Якщо це перший раз, коли ми натрапили на нуль
                            if (waves[x, y] == 0)
                            {
                                waves[x, y] = currentWave; 
                                replace[x, y] = replace[cur.X, cur.Y]; // Копіюємо значення з сусідньої ненульової клітинки
                                nextQueue.Add(new Point(x, y));
                            }
                            else if (waves[x, y] == currentWave && replace[x, y] != replace[cur.X, cur.Y])
                            {
                                replace[x, y] = MARKED; 
                            }
                        }
                        else if (waves[x, y] == 0)
                        {
                            // Якщо натрапили на ненульовий елемент, додаємо його в чергу
                            waves[x, y] = currentWave;
                            nextQueue.Add(new Point(x, y));
                        }
                    }
                }
            }
            currentQueue = new List<Point>(nextQueue);
            nextQueue.Clear();
            currentWave++;
        }

        // Заповнюємо розв'язок
        for (var i = 0; i < matrixSize; i++)
        {
            for (var j = 0; j < matrixSize; j++)
            {
                if (matrix[i, j] == 0)
                {
                    // Якщо клітинка позначена, залишаємо 0
                    if (replace[i, j] == MARKED)
                    {
                        solution[i, j] = 0;
                    }
                    else
                    {
                        solution[i, j] = replace[i, j];
                    }
                }
                else
                {
                    solution[i, j] = matrix[i, j];
                }
            }
        }

        return solution;
    }
}
