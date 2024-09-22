using App;

namespace Test;

public class IOHandlerTest
{
    [Fact]
    public void ReadOrders()
    {
        var expectedResult = new Order[]
        {
            new(2, 25),
            new(3, 45)
        };

        var filePath = Path.Combine("TestData", "test_reading.txt");
        var actualResult = IOHandler.ReadOrders(filePath);

        Assert.Equal(expectedResult, actualResult);
    }
}
