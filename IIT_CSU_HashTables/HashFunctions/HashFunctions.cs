namespace IIT_CSU_HashTables.HashFunctions;

public static class HashFunctions
{
    public static int StdCombine(int key)
        => Math.Abs(HashCode.Combine(key) % 1000);

    public static int TwoPowTen(int key)
        => (key << 10) % 1000;
    
    public static int MultiplyMethod(int key)
    {
        const int m = 1000;
        double a = (Math.Sqrt(5) - 1) / 2;
        return (int)Math.Floor(m * (key * a % 1));
    }

    public static int ModMethod(int key)
        => key % 1000;

    public static int DigitsSum(int key)
    {
        string strKey = key.ToString();
        int result = 0;
        for (int i = 0; i < strKey.Length - 1; i += 2)
        {
            result += int.Parse($"{strKey[i]}{strKey[i + 1]}");
        }

        if (result >= 1000)
        {
            result %= 1000;
        }

        return result;
    }
}

public delegate int HashFunction(int key);