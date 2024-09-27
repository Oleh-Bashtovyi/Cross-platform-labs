namespace App;

public static class BlocksCombiningProblemSolver
{
    public const int MIN_LEFT = 1;
    public const int MAX_LEFT = 100;
    public const int MIN_RIGHT = 1;
    public const int MAX_RIGHT = 100;

    public static int Solve(ProductBlock[] blocks)
    {
        if (blocks.Length == 0 || blocks.Length == 1)
        {
            return 0;
        }

        var validator = new ProductBlockValidator(MIN_LEFT, MAX_LEFT, MIN_RIGHT, MAX_RIGHT);

        validator.Validate(blocks);
        
        var n = blocks.Count();
        var leftParts = new int[n + 1];
        var rightParts = new int[n + 1];

        for ( var i = 0; i < n; i++)
        {
            leftParts[i + 1] = blocks[i].LeftPart;
            rightParts[i + 1] = blocks[i].RightPart;
        }

        var cost = new int[n + 1, n + 1];

        for (int len = 1; len <= n; len++)
        {
            for (int left = 1; left + len - 1 <= n; left++)
            {
                int right = left + len - 1;

                if (len == 1)
                {
                    cost[left, right] = 0;
                }
                else
                {
                    int min = 1000 * 1000 * 1000;

                    for (int right1 = left; right1 < right; right1++)
                    {
                        int left2 = right1 + 1;
                        min = Math.Min(min, cost[left, right1] + cost[left2, right]);
                    }

                    cost[left, right] = min + leftParts[left] * rightParts[right];
                }

            }
        }

        return cost[1, n];
    }

}
