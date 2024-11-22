using Lab13.Server.Extensions;
using Lab13.Server.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Lab13.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LabsController : ControllerBase
{
    [HttpGet("lab1")]
    public IActionResult Lab1()
    {
        var testCase1 = new TestCase()
        {
            Input = "2\n" +
                    "1 10\n" +
                    "2 12",

            Output = "22"
        };

        var testCase2 = new TestCase()
        {
            Input = "3\n" +
                    "1 10\n" +
                    "1 20\n" +
                    "3 24",

            Output = "44"
        };

        var model = new LabViewModel()
        {
            LabNumber = 1,
            TaskVariant = "44",
            Description = "Антон працює кур'єром. Має багато замовлень. На виконання одного замовлення у Антона йде рівно один день. " +
                          "Для кожного замовлення визначено вартість та термін його виконання (кількість днів, що залишилися до " +
                          "запланованого дня виконання замовлення). Якось прокинувшись, Антон вивчив свій графік і зрозумів, " +
                          "що, можливо, він не зможе виконати всі замовлення, і його можуть звільнити. " +
                          "Тому він вирішив виконати лише деякі з них так, щоб отримати максимальний дохід.",
            InputDescription = "Перший рядок вхідного файлу INPUT.TXT містить ціле число N (1 ≤ N ≤ 1000) – кількість замовлень. " +
                               "Потім у N рядках описані дані кожного замовлення Ti та Ci (натуральні числа, що не перевищують 105). " +
                               "Де Ti – останній день, коли ще можна виконати замовлення, Ci – винагорода за виконання замовлення.",
            OutputDescription = "У вихідний файл OUTPUT.TXT виведіть максимальну винагороду, яку можна отримати, виконуючи замовлення.",
            TestCases = [testCase1, testCase2]
        };

        return Ok(model);
    }


    [HttpGet("lab2")]
    public IActionResult Lab2()
    {
        var testCase1 = new TestCase()
        {
            Input = "3\n" +
                    "34 29\n" +
                    "29 4\n" +
                    "4 15",

            Output = "646"
        };

        var model = new LabViewModel()
        {
            LabNumber = 2,
            TaskVariant = "44",
            Description = "Виріб виготовляють із n блоків, кожен з яких має два технологічні параметри – mi та ki. " +
                          "Відомо, що ki=mi+1, i=1, 2, …, n-1. При цій умові два послідовні блоки i та i+1 можна об'єднувати в один " +
                          "новий, який матиме технологічні параметри - mi та ki+1, і на це буде потрібно mi*ki+1 технологічних операцій. " +
                          "Новий блок можна знову поєднувати з одним із сусідніх і так далі. Змінюючи порядок збирання блоків можна " +
                          "домогтися зменшення кількості технологічних операцій.\n" +
                          "Пояснимо це на прикладі трьох блоків: 34 і 29, 29 і 4, 4 і 15.Якщо зібрати спочатку 2 і 3 блок, а потім " +
                          "приєднати зібране до першого, то потрібно 29 * 15 + 34 * 15 = 435 + 510 = 945 операцій.Якщо спочатку зібрати " +
                          "блок з 1 і 2 вихідних блоків, а потім приєднати 3 блок, то потрібно 34 * 4 + 34 * 15 = 136 + 510 = 646 " +
                          "операцій.\n" +
                          "Потрібно написати програму, яка знайде мінімальну кількість технологічних операцій виготовлення виробу.",
            InputDescription = "Вхідний файл INPUT.TXT містить у першому рядку число n – кількість блоків (1 ≤ n ≤ 100). Наступні n " +
                               "рядків містять пари чисел (розділених пропуском) – технологічні параметри блоків. " +
                               "Технологічні параметри - цілі невід'ємні числа, що не перевищують 100.",
            OutputDescription = "Вихідний текстовий файл OUTPUT.TXT повинен містити одне число – мінімальне число технологічних операцій.",
            TestCases = [testCase1]
        };

        return Ok(model);
    }



    [HttpGet("lab3")]
    public IActionResult Lab3()
    {
        var testCase1 = new TestCase()
        {
            Input = "3\n" +
                    "0 0 0\n" +
                    "1 0 2\n" +
                    "0 3 0",

            Output = "1 0 2\n" +
                     "1 0 2\n" +
                     "0 3 0",
        };

        var model = new LabViewModel()
        {
            LabNumber = 3,
            TaskVariant = "44",
            Description = "Дано матрицю A розміром N×N, заповнену невід'ємними цілими числами. " +
                          "Відстань між двома елементами Ai j та Ap q визначено як | i - p | + | j - q |. " +
                          "Потрібно замінити кожен нульовий елемент матриці найближчим ненульовим. " +
                          "Якщо є два або більше найближчих ненульових осередків, " +
                          "нуль має бути залишений." +
                          "Потрібно написати програму, яка знайде мінімальну кількість технологічних операцій виготовлення виробу. ",
            InputDescription = "У першому рядку вхідного файлу INPUT.TXT міститься натуральне число N (N ≤ 200). " +
                               "Потім йдуть N рядків N чисел, розділених пробілами і являють собою матрицю. " +
                               "Елементи матриці не перевищують значення 106.",
            OutputDescription = "У вихідний файл OUTPUT.TXT виведіть N рядків N чисел, розділених пробілами, - модифіковану матрицю.",
            TestCases = [testCase1]
        };

        return Ok(model);
    }


    [HttpPost("process-lab")]
    public async Task<IActionResult> ProcessLab([FromForm]int labNumber, [FromForm]IFormFile inputFile)
    {
        if (inputFile == null || inputFile.Length == 0)
        {
            return BadRequest(new { Error = "Please upload a file" });
        }

        if (labNumber != 1 && labNumber != 2 && labNumber != 3)
        {
            return BadRequest(new { Error = "Invalid lab number. Available lab numbers: 1, 2, 3." });
        }

        var tempFilePath = Path.GetTempFileName();

        try
        {
            using (var stream = new FileStream(tempFilePath, FileMode.Create))
            {
                await inputFile.CopyToAsync(stream);
            }

            switch (labNumber)
            {
                case 1:
                    var orders = Lab_1.IOHandler.ReadOrders(tempFilePath);
                    var res1 = Lab_1.OrdersProblemSolver.Solve(orders);
                    return Ok(new { output = res1 });
                case 2:
                    var blocks = Lab_2.IOHandler.ReadProductBlocks(tempFilePath);
                    var res2 = Lab_2.BlocksCombiningProblemSolver.Solve(blocks.ToArray());
                    return Ok(new { output = res2 });
                case 3:
                    var matrix = Lab_3.IOHandler.ReadMatrixFromFile(tempFilePath);
                    var res3 = Lab_3.Solution.Solve(matrix);
                    return Ok(new { output = res3.ToFormattedString() });
                default:
                    return BadRequest(new { Error = "Invalid lab number. Available lab numbers: 1, 2, 3." });
            }
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
        finally 
        {
            if (System.IO.File.Exists(tempFilePath))
            {
                System.IO.File.Delete(tempFilePath);
            }
        }
    }

}
