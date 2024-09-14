using Lab_1;

namespace Lab_1_Test;

public class GreedSolverTest
{
    [Fact]
    public void SolveOrdersProblem_ExampleTestCase_1()
    {
        var orders = new Order[]
        {
            new(1, 10),
            new(2, 12),
        };

        var actualResult = OrdersProblemSolver.Solve(orders);
        var expectedResult = 22;

        Assert.Equal(expectedResult, actualResult);
    }


    [Fact]
    public void SolveOrdersProblem_ExampleTestCase_2()
    {
        var orders = new Order[]
        {
            new(1, 10),
            new(1, 20),
            new(3, 24),
        };

        var actualResult = OrdersProblemSolver.Solve(orders);
        var expectedResult = 44;

        Assert.Equal(expectedResult, actualResult);
    }


    [Theory]
    [InlineData("test_1.txt", 650)]
    [InlineData("test_2.txt", 200000000)]
    [InlineData("test_3.txt", 6275)] 
    [InlineData("test_4.txt", 20010)]
    [InlineData("test_5.txt", 32750)]
    public void SolveOrdersProblem_ReadAndSolve(string fileName, int expectedResult)
    {
        var filePath = Path.Combine("TestData", fileName);

        var orders = OrdersReader.Read(filePath);

        var actualResult = OrdersProblemSolver.Solve(orders);

        Assert.Equal(expectedResult, actualResult);
    }
}