var tests = new (int target, int[] nums, int expected)[]
{
    // Examples
    (7, [2, 3, 1, 2, 4, 3], 2),
    (8, [3, 3, 3], 3),
    (15, [1, 2, 3, 4], 0),

    // Edge cases
    (5, [], 0),
    (5, [5], 1),
    (5, [4], 0),
    (7, [8, 1, 1], 1),
    (6, [1, 2, 3], 3),
    (7, [1, 1, 1, 7], 1),
    (2000000000, [900000000, 900000000, 900000000], 3),
};

foreach (var (target, nums, expected) in tests)
{
    try
    {
        int actual = MinSubarrayLength(target, nums);
        string status = actual == expected ? "PASS" : "FAIL";
        Console.WriteLine($"{status}: target={target}, nums=[{string.Join(", ", nums)}] -> expected {expected}, got {actual}");

    }
    catch (Exception ex)
    {
        Console.WriteLine($"ERROR: target={target}, nums=[{string.Join(", ", nums)}] -> expected {expected}, threw {ex.GetType().Name}: {ex.Message}");

    }
}


int MinSubarrayLength(int target, int[] nums)
{
    // return 0 for empty arrays
    if (nums.Length == 0)
        return 0;

    // initialize output and window bounds
    int minSubLength = 0;
    int leftBound = 0;
    int rightBound = 0;
    long windowSum = nums[0];

    // loop through nums; looping until left bound reaches end of nums ensures every potential minimum window will be checked
    while (leftBound <= nums.Length - 1 && rightBound <= nums.Length - 1)
    {
        // establish current window size
        int windowSize = rightBound - leftBound + 1;

        // if window sum reaches target, check if it is new shortest window
        if (windowSum >= target)
        {
            // if shortest window is found, record it and move left bound to check for even shorter window
            if (windowSize <= minSubLength || minSubLength == 0)
            {
                minSubLength = windowSize;
                // if window of 1 is found, return since smaller window size won't be found
                if (minSubLength == 1)
                {
                    return minSubLength;
                }
                // remove leftmost element in window before shrinking it
                windowSum -= nums[leftBound];
                leftBound++;
            }
        }
        // if current window does not reach target and stretches to the end of the array, break since no further window will reach the target
        else if (rightBound == nums.Length - 1)
        {
            break;
        }
        // if target is not reached, move right bound to expand window and add new rightmost element
        else
        {
            rightBound++;
            windowSum += nums[rightBound];
        }
    }

    return minSubLength;
}