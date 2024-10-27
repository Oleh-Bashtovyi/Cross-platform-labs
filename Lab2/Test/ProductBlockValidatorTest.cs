using Lab_2;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Test;

public class ProductBlockValidatorTest
{
    private readonly ProductBlockValidator _validator;
    private readonly ITestOutputHelper _output;

    public ProductBlockValidatorTest(ITestOutputHelper output)
    {
        int minRight = 1;
        int maxRight = 100;
        int minLeft = 1;
        int maxLeft = 100;
        _validator = new (minLeft, maxLeft, minRight, maxRight);
        _output = output;
    }

    [Theory]
    [InlineData(5, 0)]
    [InlineData(5, 101)]
    [InlineData(0, 5)]
    [InlineData(101, 5)]
    public void Validate_InvalidOrder_ThrowsArgumentOutOfRangeException(int deadline, int reward)
    {
        var order = new ProductBlock(deadline, reward);
        var orders = new ProductBlock[1] { order };

        var error = Assert.Throws<ArgumentOutOfRangeException>(() => _validator.Validate(orders));
        _output.WriteLine(error.Message);
    }
}
