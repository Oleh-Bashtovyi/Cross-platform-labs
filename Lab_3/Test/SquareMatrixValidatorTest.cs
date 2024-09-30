using App;
using Xunit.Abstractions;

namespace Test;

public class SquareMatrixValidatorTest
{
    private readonly SquareMatrixValidator _validator;
    private readonly ITestOutputHelper _output;
    private const int MATRIX_MAX_DIMENSION = 5;
    private const int MATRIX_MIN_DIMENSION = 2;
    private const int MATRIX_MAX_VALUES = 20;
    private const int MATRIX_MIN_VALUES = 0;


    public SquareMatrixValidatorTest(ITestOutputHelper output)
    {
        _validator = new SquareMatrixValidator(MATRIX_MIN_DIMENSION, MATRIX_MAX_DIMENSION, MATRIX_MIN_VALUES, MATRIX_MAX_VALUES);
        _output = output;
    }


    [Fact]
    public void Validate_NonSquareMatrix_ThrowsException()
    {
        var matrix = new int[3, 4]
        {
            {1, 1, 1, 1, },
            {1, 1, 1, 1, },
            {1, 1, 1, 1, },
        };

        Assert.Throws<ArgumentException>(() => _validator.Validate(matrix));
    }


    [Fact]
    public void Validate_MatrixHasValuesGraterThanMax_ThrowsArgumentOutOfRangeExceptionn()
    {
        var matrix = new int[4, 4]
        {
            {1, 1, 3, 1, },
            {1, 5, 1, 1, },
            {1, 1, 12342, 1, },
            {1, 9, 1, 7, },
        };

        Assert.Throws<ArgumentOutOfRangeException>(() => _validator.Validate(matrix));
    }


    [Fact]
    public void Validate_MatrixHasValuesLessThanMin_ThrowsArgumentOutOfRangeExceptionn()
    {
        var matrix = new int[4, 4]
        {
            {1, 1, 3, 1, },
            {1, 5, 1, 1, },
            {1, 1, -12342, 1, },
            {1, 9, 1, 7, },
        };

        Assert.Throws<ArgumentOutOfRangeException>(() => _validator.Validate(matrix));
    }
}
