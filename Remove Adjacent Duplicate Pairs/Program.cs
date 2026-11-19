var tests = new (string s, string expected)[]
{
    // Examples
    ("abbaca", "ca"),
    ("azxxzy", "ay"),
    ("abc", "abc"),

    // Edge cases
    ("", ""),
    ("a", "a"),
    ("aa", ""),
    ("aaa", "a"),
    ("abba", ""),
    ("abab", "abab"),
    ("aaaa", ""),
};

foreach (var (s, expected) in tests)
{
    try
    {
        string actual = RemoveAdjacentPairs(s);
        string status = actual == expected ? "PASS" : "FAIL";
        Console.WriteLine($"{status}: \"{s}\" -> expected \"{expected}\", got \"{actual}\"");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"ERROR: \"{s}\" -> expected \"{expected}\", threw {ex.GetType().Name}: {ex.Message}");
    }
}


string RemoveAdjacentPairs(string s)
{
    // handle empty string
    if (s == "") return "";

    // initialize output stack
    Stack<char> noAdjacents = new Stack<char>();

    foreach (char c in s)
    {
        if (noAdjacents.Count == 0 || c != noAdjacents.Peek())
        {
            noAdjacents.Push(c);
        }
        else
        {
            noAdjacents.Pop();
        }
    }

    char[] outputArray = noAdjacents.ToArray();
    Array.Reverse(outputArray);
    string reversedOutput = new string(outputArray);
    return reversedOutput;
}