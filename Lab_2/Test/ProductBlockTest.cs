using App;

namespace Test;

public class ProductBlockTest
{
    [Fact]
    public void Equal_True()
    {
        var instance_1 = new ProductBlock(1, 15);
        var instance_2 = new ProductBlock(1, 15);
        Assert.Equal(instance_1, instance_2);
    }

    [Fact]
    public void Equal_False()
    {
        var instance_1 = new ProductBlock(1, 15);
        var instance_2 = new ProductBlock(1, 25);
        Assert.NotEqual(instance_1, instance_2);
    }

    [Fact]
    public void ProductBlock_ToString()
    {
        var instance = new ProductBlock(1, 15);
        var actual = instance.ToString();
        var expected = "[1, 15]";
        Assert.Equal(expected, actual); 
    }
}
