using Lab_1;

namespace Lab_1_Test;

public class OrderTest
{
    [Fact]
    public void Equal_True()
    {
        var instance_1 = new Order(1, 15);
        var instance_2 = new Order(1, 15);
        Assert.Equal(instance_1, instance_2);
    }

    [Fact]
    public void Equal_False()
    {
        var instance_1 = new Order(1, 15);
        var instance_2 = new Order(1, 25);
        Assert.NotEqual(instance_1, instance_2);
    }

    [Fact]
    public void Order_ToString()
    {
        var instance = new Order(1, 15);
        var actual = instance.ToString();
        var expected = "[1, 15]";
        Assert.Equal(expected, actual); 
    }
}
