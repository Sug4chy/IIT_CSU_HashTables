namespace IIT_CSU_HashTables;

public static class PairsGenerator
{
    public static IEnumerable<KeyValuePair<int, string>> Generate(int count)
    {
        var result = new List<KeyValuePair<int, string>>();
        for (int i = 1; i <= count; i++)
        {
            result.Add(new KeyValuePair<int, string>(i, i.ToString()));
        }

        return result;
    }
}