using IIT_CSU_HashTables.HashTables;

namespace IIT_CSU_HashTables;

internal static class Program
{
    public static void Main()
    {
        var hashTable = new ChainHashTable<string>(HashFunctions.EMethod);
        var pairs = PairsGenerator.Generate(100_000);
        foreach (var pair in pairs)
        {
            hashTable.Add(pair.Key, pair.Value);
        }
        
        Console.WriteLine(hashTable.GetFillFactor());
        Console.WriteLine(hashTable.GetLengthOfTheLongestChain());
        Console.WriteLine(hashTable.GetLengthOfTheShortestChain());
    }
}