using System.Drawing;

namespace App;

public static class Solution
{
    public const int MATRIX_MAX_DIMENSION = 200;
    public const int MATRIX_MIN_DIMENSION = 0;
    public const int MATRIX_MAX_VALUES = 1_000_000;
    public const int MATRIX_MIN_VALUES = 0;
    private const int MARKED = -1000000000;
    private static readonly SquareMatrixValidator s_Validator;
    private static readonly Point[] Directions =
    [
        new Point(-1, 0), // вгору
        new Point(0, -1), // вліво
        new Point(1, 0),  // вниз
        new Point(0, 1)   // вправо
    ];







/*    private static readonly int[] moveX = { -1, 0, 1, 0 };
    private static readonly int[] moveY = { 0, -1, 0, 1 };*/

/*    private class ViewPoint
    {
        public int x { get; set; } 
        public int y { get; set; } 
        public int num { get; set; } 

        public ViewPoint() { }

        public ViewPoint(int X, int Y, int Num)
        {
            x = X;
            y = Y;
            num = Num;
        }

        public override bool Equals(object obj)
        {
            if (obj is ViewPoint other)
            {
                return x == other.x && y == other.y;
            }
            return false;
        }


        public override int GetHashCode()
        {
            return (x, y).GetHashCode();
        }
    }*/








    static Solution()
    {
        s_Validator = new SquareMatrixValidator(MATRIX_MIN_DIMENSION, MATRIX_MAX_DIMENSION, MATRIX_MIN_VALUES, MATRIX_MAX_VALUES);
    }



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


        var currentQueue = new List<Point>();
        var nextQueue = new List<Point>();


        var waves = new int[matrixSize, matrixSize];
        var replace = new int[matrixSize, matrixSize];

        //Шукаєм ненульові елементи, з яких почнеться поширення хвиль (BFS)
        for (var i = 0; i < matrixSize; i++)
        {
            for (var j = 0; j < matrixSize; j++)
            {
                if(matrix[i, j] != 0)
                {
                    currentQueue.Add(new Point(i, j));
                    replace[i, j] = matrix[i, j];
                    waves[i, j] = 1;
                }
            }
        }

        var currentWave = 2;

        //var IsInMatrix = (Point p) => !(p.X < 0 || p.Y < 0 || p.X >= matrixSize || p.Y >= matrixSize);
        var IsInMatrix = (int x, int y) => !(x < 0 || y < 0 || x >= matrixSize || y >= matrixSize);


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








    /*    public static int[,] Solve(int[,] matrix)
        {
            s_Validator.Validate(matrix);

            var matrixSize = matrix.GetLength(0);

            if (matrixSize == 0)
            {
                return new int[0, 0];
            }

            if(matrixSize == 1)
            {
                return new int[1, 1] { { matrix[0, 0] } };
            }

            var solution = new int[matrixSize, matrixSize];

            //....



            var n = matrix.GetLength(0);
            // Основна матриця та допоміжна для зберігання "хвиль"
            var mas = matrix;
            var waves = new int[n, n];

            // Черга для обробки точок та список початкових ненульових точок
            var mainQueue = new List<ViewPoint>();
            var initPoints = new List<Point>();

            int amount = 1;

            // 1. Ініціалізація початкових точок
            // Проходимо по всій матриці, шукаємо ненульові елементи
            // і додаємо їх у чергу та список початкових точок.
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (mas[i, j] != 0)
                    {
                        // Додаємо координати ненульового елемента в список початкових точок
                        initPoints.Add(new Point(i, j));

                        // Додаємо точку до основної черги з її номером
                        mainQueue.Add(new ViewPoint(i, j, amount++));
                    }
                }
            }

            // Наступна черга для обробки точок
            List<ViewPoint> nextQueue;

            // Поточна кількість "хвиль" (раундів обробки)
            int wavesAmount = 1;

            var Correct = (int x, int y) => !(x < 0 || y < 0 || x >= n || y >= n);


            // 2. Основний алгоритм поширення хвиль навколо ненульових елементів
            do
            {
                // Очищаємо чергу для наступного раунду
                nextQueue = new List<ViewPoint>();

                // Обробляємо всі точки в основній черзі
                while (mainQueue.Count > 0)
                {
                    // Беремо останню точку з черги для обробки
                    var cur = mainQueue[mainQueue.Count - 1];
                    mainQueue.RemoveAt(mainQueue.Count - 1);

                    // Проходимо по всіх чотирьох напрямках (вгору, вниз, вліво, вправо)
                    for (int i = 0; i < 4; i++)
                    {
                        int x = cur.x + moveX[i];
                        int y = cur.y + moveY[i];

                        // Якщо координати в межах матриці
                        if (Correct(x, y))
                        {
                            // Якщо поточний елемент — нульовий (вперше)
                            if (mas[x, y] == 0)
                            {
                                // Присвоюємо йому негативний номер з черги (або маркуємо, якщо -1)
                                mas[x, y] = cur.num == -1 ? MARKED : -cur.num;

                                // Додаємо нову точку до наступної черги
                                nextQueue.Add(new ViewPoint(x, y, cur.num));

                                // Вказуємо поточну хвилю для цієї точки
                                waves[x, y] = wavesAmount;
                            }
                            // Якщо елемент вже оброблявся в цю хвилю і його номер відрізняється
                            else if (waves[x, y] == wavesAmount && mas[x, y] != -cur.num && mas[x, y] != MARKED)
                            {
                                // Маркуємо його як особливий випадок
                                mas[x, y] = MARKED;

                                // Шукаємо його в черзі і змінюємо його статус на -1
                                var newViewPoint = new ViewPoint(x, y, -1);
                                for (int k = 0; k < nextQueue.Count; k++)
                                {
                                    if (nextQueue[k] == newViewPoint)
                                    {
                                        nextQueue[k].num = -1;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                // Міняємо місцями черги для наступної хвилі
                mainQueue = new List<ViewPoint>(nextQueue);
                wavesAmount++;

            } while (mainQueue.Count > 0); // Поки є точки для обробки

            // 3. Виведення результату та заміна маркованих значень
            // Після завершення хвиль проходимо по всій матриці та замінюємо
            // марковані точки або заповнюємо їх найближчими ненульовими елементами
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    // Якщо елемент був помічений як MARKED, змінюємо його на 0
                    if (mas[i, j] == MARKED)
                        mas[i, j] = 0;

                    // Якщо елемент негативний, заповнюємо його значенням з початкової точки
                    else if (mas[i, j] < 0)
                    {
                        Point p = initPoints[-mas[i, j] - 1];
                        mas[i, j] = mas[p.x, p.y];
                    }
                }
            }

            return mas;




            //...






            return solution;
        }*/
}
