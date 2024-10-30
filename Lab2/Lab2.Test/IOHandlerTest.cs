using Lab_2;

namespace Test;

public class IOHandlerTest
{
    [Theory]
    [InlineData("test_blocks_count_out_of_range_max.txt")]
    [InlineData("test_blocks_count_out_of_range_min.txt")]
    public void ReadOrders_OrdersNumberOutOfRange_ThrowsArgumentOutOfRangeException(string fileName)
    {
        var filePath = Path.Combine("TestData", fileName);

        Assert.Throws<InvalidOperationException>(() => IOHandler.ReadProductBlocks(filePath));
    }

    [Fact]
    public void ReadOrders()
    {
        var expectedResult = new ProductBlock[]
        {
            new(34, 29),
            new(29, 4),
            new(4, 15)
        };

        var filePath = Path.Combine("TestData", "test_reading.txt");
        var actualResult = IOHandler.ReadProductBlocks(filePath);

        Assert.Equal(expectedResult, actualResult);
    }
}
