namespace App;

public static class OrdersProblemSolver
{
    public const int MIN_REWARD = 0;
    public const int MAX_REWARD = 100_000;
    public const int MIN_DEADLINE = 1;
    public const int MAX_DEADLINE = 100_000;

    /// <summary>
    /// Calculate the maximum reward that can be obtained by fulfilling the orders.
    /// </summary>
    /// <param name="orders"></param>
    /// <returns>maximum reward</returns>
    public static int Solve(IEnumerable<Order> orders)
    {
        if (orders.Count() == 0)
        {
            return 0;
        }

        foreach (var order in orders)
        {
            ValidateOrder(order);
        }

        var sortedOrders = orders.OrderByDescending(x => x.Reward).ToList();

        var occupied = new bool[MAX_DEADLINE + 1];

        var totalReward = 0;

        foreach(var order in sortedOrders)
        {
            // find the nearest free day before the deadline
            for (int day = order.Deadline; day > 0; day--)
            {
                if (!occupied[day])
                {
                    occupied[day] = true; 
                    totalReward += order.Reward;
                    break;
                }
            }
        }

        return totalReward;
    }


    private static void ValidateOrder(Order order)
    {
        if(order.Reward < MIN_REWARD || order.Reward > MAX_REWARD)
        {
            throw new ArgumentOutOfRangeException(
                nameof(Order.Reward),  
                $"Reward should be between {MIN_REWARD} and {MAX_REWARD}." + Environment.NewLine +
                $"Actual reward: {order.Reward}, order: {order}");
        }
        if (order.Deadline < MIN_DEADLINE || order.Deadline > MAX_DEADLINE)
        {
            throw new ArgumentOutOfRangeException(
                nameof(Order.Deadline),
                $"Deadline should be between {MIN_DEADLINE} and {MAX_DEADLINE}." + Environment.NewLine +
                $"Actual deadline: {order.Deadline}, order: {order}");
        }
    }
}
