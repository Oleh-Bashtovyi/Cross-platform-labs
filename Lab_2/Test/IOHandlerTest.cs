using App;

namespace Test;

public class IOHandlerTest
{
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
