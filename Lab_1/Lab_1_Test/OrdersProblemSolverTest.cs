using App;
using Xunit.Abstractions;

namespace Test;

public class OrdersProblemSolverTest(ITestOutputHelper output)
{
    ITestOutputHelper _output = output;

    [Fact]
    public void SolveOrdersProblem_ExampleTestCase_1()
    {
        //Arrange
        var orders = new Order[]
        {
            new(1, 10),
            new(2, 12),
        };
        var expectedResult = 22;

        //Act
        var actualResult = OrdersProblemSolver.Solve(orders);

        //Assert
        Assert.Equal(expectedResult, actualResult);
        _output.WriteLine($"Example test case 1 solved: " +
                          $"{orders[0]}, {orders[1]}, " +
                          $"max reward = {expectedResult}");
    }


    [Fact]
    public void SolveOrdersProblem_ExampleTestCase_2()
    {
        //Arrange
        var orders = new Order[]
        {
            new(1, 10),
            new(1, 20),
            new(3, 24),
        };
        var expectedResult = 44;

        //Act
        var actualResult = OrdersProblemSolver.Solve(orders);

        //Assert
        Assert.Equal(expectedResult, actualResult);
        _output.WriteLine($"Example test case 2 solved: " +
                          $"{orders[0]}, {orders[1]}, {orders[2]}; " +
                          $"max reward = {expectedResult}");
    }

/*
    [Theory]
    [InlineData("test_1.txt", 650)]
    [InlineData("test_2.txt", 200000000)]
    [InlineData("test_3.txt", 6275)]
    [InlineData("test_4.txt", 20010)]
    [InlineData("test_5.txt", 32750)]
    public void SolveOrdersProblem_ReadAndSolve(string fileName, int expectedResult)
    {
        var filePath = Path.Combine("TestData", fileName);

        var orders = IOHandler.ReadOrders(filePath);

        var actualResult = OrdersProblemSolver.Solve(orders);

        Assert.Equal(expectedResult, actualResult);
    }*/



    [Theory]
    [InlineData(650, new int[] { 2, 100, 1, 50, 2, 150, 3, 200, 2, 300 })]
    [InlineData(200_000_000, new int[] { 5, 100_000_000, 5, 50_000_000, 5, 30_000_000, 5, 20_000_000 })]
    public void SolveOrdersProblem_ReadAndSolve(int expectedResult, int[] data)
    {
        //Arrange
        var orders = new List<Order>();

        for (int i = 0; i < data.Length; i += 2)
        {
            var order = new Order(data[i], data[i + 1]);
            orders.Add(order);
        }

        //Act
        var actualResult = OrdersProblemSolver.Solve(orders);

        //Assert
        Assert.Equal(expectedResult, actualResult);
    }
}