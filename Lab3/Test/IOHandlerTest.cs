using App;
namespace Test;

public class IOHandlerTest
{
    [Fact]
    public void ReadMatrix_MatrixSizeOutOfRange_ThrowsInvalidOperationException()
    {
        var filePath = Path.Combine("TestData", "test_matrix_size_out_of_max_size");

        Assert.Throws<InvalidOperationException>(() => IOHandler.ReadMatrixFromFile(filePath));
    }

    [Fact]
    public void ReadMatrix()
    {
        var expectedResult = new int[,]
        {
            {1, 2, 3, 4 },
            {5, 6 , 7, 8 },
            {9, 10 , 11 , 12 },
            {13, 14 , 15 , 16 }
        };

        var filePath = Path.Combine("TestData", "test_reading.txt");
        var actualResult = IOHandler.ReadMatrixFromFile(filePath);

        Assert.Equal(expectedResult, actualResult);
    }
}
