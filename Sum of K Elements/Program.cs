var tests = new (int[] nums, int k, long expected)[]
{
    (new[] { 2, 1, 5, 1, 3, 2 }, 3, 9),
    (new[] { -8, -3, -6, -2 }, 2, -8),
    (new[] { 4, -1, 2 }, 3, 5),
    (new[] { 7 }, 1, 7),
    (new[] { 1, 2, 8, 9 }, 2, 17),
    (new[] { 0, 0, 0 }, 2, 0),
    (new[] { 1_000_000_000, 1_000_000_000, 1_000_000_000 }, 3, 3_000_000_000),
    (new[] { -5, -2, -9 }, 1, -2),
};

foreach (var (nums, k, expected) in tests)
{
    long actual = MaxWindowSum(nums, k);
    string status = actual == expected ? "PASS" : "FAIL";
    Console.WriteLine($"{status}: [{string.Join(", ", nums)}], k={k} -> expected {expected}, got {actual}");
}


long MaxWindowSum(int[] nums, int k)
{
    // initialize output with the sum of the first k numbers
    long maxSum = 0;
    for (int h = 0; h < k; h++)
    {
        maxSum += nums[h];
    }

    long currentSum = maxSum; // tracks sum of current window to be compared against maxSum
    for (int i = k; i < nums.Length; i++)
    {
        currentSum += nums[i];
        currentSum -= nums[i-k];
        if (currentSum > maxSum)
        {
            maxSum = currentSum;
        }
    }

    return maxSum;
}