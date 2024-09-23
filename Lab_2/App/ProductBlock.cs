namespace App;

public class ProductBlock
{
    public int LeftPart {  get; set; }
    public int RightPart {  get; set; }


    public ProductBlock() { }

    public ProductBlock(int leftPart, int rightPart)
    {
        LeftPart = leftPart;
        RightPart = rightPart;
    }

    public override bool Equals(object? obj)
    {
        if (obj is ProductBlock other)
        {
            return LeftPart == other.LeftPart && RightPart == other.RightPart;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return $"[{LeftPart}, {RightPart}]";
    }
}
