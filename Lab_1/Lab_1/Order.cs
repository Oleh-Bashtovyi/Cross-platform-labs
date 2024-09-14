namespace Lab_1;

public class Order
{
    public int Deadline {  get; set; }
    public int Profit {  get; set; }

    public Order() { }

    public Order(int deadline, int profit)
    {
        Deadline = deadline;
        Profit = profit;
    }

    public override bool Equals(object? obj)
    {
        if (obj is Order other)
        {
            return Deadline == other.Deadline && Profit == other.Profit;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
