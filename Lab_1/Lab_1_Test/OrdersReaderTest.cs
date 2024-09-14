using Lab_1;

namespace Lab_1_Test;

public class OrdersReaderTest
{
    [Fact]
    public void ReadOrders()
    {
        var expectedResult = new Order[]
        {
            new(2, 25),
            new(3, 45)
        };

        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"TestData\test_1.txt");
        var actualResult = OrdersReader.ReadOrders(filePath);

        Assert.Equal(expectedResult, actualResult);
    }
}
