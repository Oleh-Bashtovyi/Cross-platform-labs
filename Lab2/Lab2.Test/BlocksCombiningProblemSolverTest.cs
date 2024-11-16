using Lab_2;
using Xunit.Abstractions;

namespace Test;

public class BlocksCombiningProblemSolverTest(ITestOutputHelper output)
{
    private readonly ITestOutputHelper _output = output;

    [Fact]
    public void Solve_ExampleTestCase_1_ToBeSuccessful()
    {
        //Arrange
        var blocks = new ProductBlock[]
        {
            new(34, 29),
            new(29, 4),
            new(4, 15)
        };
        var expectedResult = 646;

        //Act
        var actualResult = BlocksCombiningProblemSolver.Solve(blocks);

        //Assert
        Assert.Equal(expectedResult, actualResult);
        _output.WriteLine($"Example test case solved: " +
                          $"{string.Join(", ", blocks.Select(b => b.ToString()))}" +
                          $"min combining cost = {expectedResult}");
    }


    [Theory]
    [InlineData(0, new int[] {12, 6})]
    [InlineData(60, new int[] {12, 6, 6, 5})]
    [InlineData(124, new int[] { 4, 15, 15, 24, 24, 7 })]
    [InlineData(2074, new int[] {100, 8, 8, 14, 14, 32, 32, 17})]
    [InlineData(179, new int[] {3, 25, 25, 47, 47, 5, 5, 13})]
    public void Solve_SimpleCases_ToBeSuccessful(int expectedResult, int[] data)
    {
        //Arrange
        var blocks = new List<ProductBlock>(data.Length / 2);

        for (int i = 0; i < data.Length; i += 2)
        {
            var block = new ProductBlock(data[i], data[i + 1]);
            blocks.Add(block);
        }

        //Act
        var actualResult = BlocksCombiningProblemSolver.Solve(blocks.ToArray());

        //Assert
        Assert.Equal(expectedResult, actualResult);
        _output.WriteLine($"Simple case: {string.Join(", ", blocks)};");
        _output.WriteLine($"Expected: {expectedResult};");
        _output.WriteLine($"Actual: {actualResult};");
    }

    [Fact]
    public void Solve_MaxNumberOfBlocksWithMaxConnectionCost_ToBeSuccessful()
    {
        var maxNumberOfBlocks = 100;
        var maxLeft = 100;
        var maxRight = 100;
        var expectedResult = 990_000;

        var blocks = new ProductBlock[maxNumberOfBlocks];

        for (int i = 0; i < maxNumberOfBlocks; i++)
        {
            blocks[i] = new ProductBlock(maxLeft, maxRight);
        }

        var actualResult = BlocksCombiningProblemSolver.Solve(blocks);

        Assert.Equal(expectedResult, actualResult);
        _output.WriteLine($"Edge case: number of blocks = {maxNumberOfBlocks}");
        _output.WriteLine($"Every block has Left = {maxLeft} and Right = {maxRight}");
        _output.WriteLine($"Expected: {expectedResult};");
        _output.WriteLine($"Actual: {actualResult};");
    }
}