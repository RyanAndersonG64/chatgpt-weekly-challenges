using System.Linq;

var tests = new (int[] temperatures, int[] expected)[]
{
    // Examples
    ([73, 74, 75, 71, 69, 72, 76, 73], [1, 1, 4, 2, 1, 1, 0, 0]),
    ([30, 40, 50, 60], [1, 1, 1, 0]),
    ([70, 70, 71], [2, 1, 0]),

    // Edge cases
    ([], []),
    ([70], [0]),
    ([80, 70, 60], [0, 0, 0]),
    ([70, 70, 70], [0, 0, 0]),
    ([70, 60, 70], [0, 1, 0]),
    ([75, 70, 65, 80], [3, 2, 1, 0]),
    ([75, 70, 75, 80], [3, 1, 1, 0]),

};

foreach (var (temperatures, expected) in tests)
{
    try
    {
        int[] actual = DaysUntilWarmer(temperatures);
        string status = actual.SequenceEqual(expected) ? "PASS" : "FAIL";
        Console.WriteLine($"{status}: temperatures=[{string.Join(", ", temperatures)}] -> expected {string.Join(", ", expected)}, got {string.Join(", ", actual)}");

    }
    catch (Exception ex)
    {
        Console.WriteLine($"ERROR:nums=[{string.Join(", ", temperatures)}] -> expected {string.Join(", ", expected)}, threw {ex.GetType().Name}: {ex.Message}");

    }
}


int[] DaysUntilWarmer(int[] temperatures)
{
    // handle empty array
    if (temperatures.Length == 0)
        return [];

    // initialize output array with same number of potisions as temperatures array
    int[] output = new int[temperatures.Length];

    // initialize stack with index 0, since there is no previous index to compare temperatures to
    Stack<int> stack = new Stack<int>();
    stack.Push(0);

    // loop through temperatures, starting at index 1 since stack was initalized with entry from index 0
    for (int i = 1; i < temperatures.Length; i++)
    {
        // repeat the check until a tempurature that is not less than the current one is found
        while (stack.Count > 0)
        {
            // if current temperature is warmer than a previous one, make that one's corresponding output the difference in indices
            if (temperatures[stack.Peek()] < temperatures[i])
            {
                output[stack.Peek()] = i - stack.Pop();
            }
            // when a temperature warmer or equal to current one is found, break the while loop
            else
            {
                break;
            }
        }
        stack.Push(i);
    }
    return output;
}