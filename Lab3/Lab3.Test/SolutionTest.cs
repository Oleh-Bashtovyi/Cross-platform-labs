using Lab_3;
using Xunit.Abstractions;

namespace Test;


public class SolutionTest(ITestOutputHelper output)
{
    private readonly ITestOutputHelper _output = output;

    [Fact]
    public void Solve_ExampleTestCase_ToBeSuccessful()
    {
        //Arrange
        var matrix = new int[3, 3]
        {
            {0, 0, 0 },
            {1, 0, 2 },
            {0, 3, 0 },
        };

        var expectedResult = new int[3, 3]
        {
            {1, 0, 2 },
            {1, 0, 2 },
            {0, 3, 0 },
        };

        //Act
        var actualResult = Solution.Solve(matrix);

        _output.WriteLine("Example test case:");
        PrintMatrix(matrix);
        _output.WriteLine("Actual:");
        PrintMatrix(actualResult);
        _output.WriteLine("Expected:");
        PrintMatrix(expectedResult);

        //Assert
        Assert.Equal(expectedResult, actualResult);
    }


    public static IEnumerable<object[]> GetMatrixTestData()
    {
        yield return new object[]
        {
            new int[4, 4]
            {
                { 3, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 1, 0, 2 },
                { 0, 0, 0, 0 }
            },
            new int[4, 4]
            {
                { 3, 3, 3, 2 },
                { 3, 1, 0, 2 },
                { 1, 1, 0, 2 },
                { 1, 1, 0, 2 }
            }
        };

        yield return new object[]
        {
            new int[4, 4]
            {
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 1, 0, 0 },
                { 0, 0, 0, 0 }
            },
            new int[4, 4]
            {
                { 1, 1, 1, 1 },
                { 1, 1, 1, 1 },
                { 1, 1, 1, 1 },
                { 1, 1, 1, 1 }
            }
        };

        yield return new object[]
        {
            new int[4, 4]
            {
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 }
            },
            new int[4, 4]
            {
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 }
            },
        };

        yield return new object[]
        {
            new int[4, 4]
            {
                { 0, 0, 2, 0 },
                { 0, 1, 0, 4 },
                { 3, 0, 0, 0 },
                { 5, 0, 0, 0 },
            },
            new int[4, 4]
            {
                { 0, 0, 2, 0 },
                { 0, 1, 0, 4 },
                { 3, 0, 0, 4 },
                { 5, 5, 5, 4 }
            },
        };

        yield return new object[]
        {
            new int[5, 5]
            {
                { 1, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 3 },
                { 0, 0, 0, 0, 0 },
                { 0, 2, 0, 0, 6 },
                { 0, 4, 0, 5, 0 },
            },
            new int[5, 5]
            {
                { 1, 1, 1, 3, 3 },
                { 1, 0, 3, 3, 3 },
                { 0, 2, 2, 0, 0 },
                { 2, 2, 2, 0, 6 },
                { 4, 4, 0, 5, 0 },
            },
        };
    }



    [Theory]
    [MemberData(nameof(GetMatrixTestData))]
    public void Solve_SimpleTestCases_ToBeSuccessful(int[,] input, int[,] expected)
    {
        // Act
        var actual = Solution.Solve(input);

        _output.WriteLine("Test case:");
        PrintMatrix(input);
        _output.WriteLine("Actual:");
        PrintMatrix(actual);
        _output.WriteLine("Expected:");
        PrintMatrix(expected);

        // Assert
        Assert.Equal(expected, actual);
    }




    private void PrintMatrix(int[,] matrix)
    {
        var matrixString = new System.Text.StringBuilder();
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            matrixString.Append(" [");

            for (int j = 0; j < cols; j++)
            {
                matrixString.Append(matrix[i, j].ToString().PadLeft(4));
                if (j < cols - 1)
                {
                    matrixString.Append(", "); 
                }
            }
            matrixString.AppendLine("]"); 
        }
        _output.WriteLine(matrixString.ToString());
    }
}
