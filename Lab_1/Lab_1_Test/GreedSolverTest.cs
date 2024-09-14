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
}