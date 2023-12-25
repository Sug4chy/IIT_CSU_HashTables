namespace IIT_CSU_HashTables.HashFunctions;

public static class BiHashFunctions
{
    public static int LinearProbing(int key, int i)
    {
        var tempHashFunction = new HashFunction(HashFunctions.StdCombine);
        return tempHashFunction(key) + 1033 * i;
    }

    public static int QuadraticProbing(int key, int i)
    {
        const int c1 = 269;
        const int c2 = 311;
        var tempHashFunction = new HashFunction(HashFunctions.StdCombine);
        return tempHashFunction(key) + c1 * i + c2 * i * i;
    }

    public static int DoubleHashing(int key, int i)
    {
        var tempHashFunction1 = new HashFunction(HashFunctions.StdCombine);
        var tempHashFunction2 = new HashFunction(HashFunctions.ModMethod);
        return tempHashFunction1(key) + i * tempHashFunction2(key);
    }

    public static int MidSquareMethod(int key, int i)
    {
        key *= key;
        string strKey = key.ToString();
        if (strKey.Length % 2 != i % 2)
        {
            i++;
        }

        int temp = strKey.Length - i * 2;
        if (temp <= 0)
        {
            return QuadraticProbing(key, i);
        }

        return key / (int)Math.Pow(10, temp) % 10;
    }

    public static int XorMethod(int key, int i)
    {
        key *= key * key;
        return key.ToString().Sum(c => c ^ i);
    }
}

public delegate int BiHashFunction(int key, int probeNumber);