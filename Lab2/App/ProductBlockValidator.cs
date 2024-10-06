namespace App;

public class ProductBlockValidator
{
    public readonly int MinLeft;
    public readonly int MaxLeft;
    public readonly int MinRight;
    public readonly int MaxRight;

    public ProductBlockValidator(int minLeft, int maxLeft, int minRight, int maxRight)
    {
        MinLeft = minLeft;
        MaxLeft = maxLeft;
        MinRight = minRight;
        MaxRight = maxRight;
    }


    public void Validate(IEnumerable<ProductBlock> blocks)
    {
        foreach (var block in blocks)
        {
            Validate(block);
        }
    }

    public void Validate(ProductBlock block)
    {
        if (block.LeftPart < MinLeft || block.LeftPart > MaxLeft)
        {
            throw new ArgumentOutOfRangeException(
                nameof(block),
                $"Left part should be between {MinLeft} and {MaxLeft}." + Environment.NewLine +
                $"Actual reward: {block.LeftPart}, product block: {block}");
        }
        if (block.RightPart < MinRight || block.RightPart > MaxRight)
        {
            throw new ArgumentOutOfRangeException(
                nameof(block),
                $"Right part should be between {MinRight} and {MaxRight}." + Environment.NewLine +
                $"Actual deadline: {block.RightPart}, product block: {block}");
        }
    }
}
