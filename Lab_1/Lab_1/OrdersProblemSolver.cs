namespace Lab_1;

public static class OrdersProblemSolver
{
    public static int Solve(IEnumerable<Order> orders)
    {
        if (orders.Count() == 0)
        {
            return 0;
        }

        var sortedOrders = orders.OrderByDescending(x => x.Reward).ToList();

        var occupied = new bool[100_001];

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

}
