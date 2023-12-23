namespace IIT_CSU_HashTables.HashFunctions;

public static class HashFunctions
{
    public static int StdCombine(int key)
        => Math.Abs(HashCode.Combine(key) % 1000);

    public static int TwoPowTen(int key)
        => (key << 10) % 1000;
    
    public static int MultiplyMethod(int key)
    {
        // m должно быть в формате m = 2^p, где p - натуральное число. Взяли максимальное,
        // при котором таблица не ломается
        const int m = 512;
        double a = (Math.Sqrt(5) - 1) / 2;
        return (int)Math.Floor(m * (key * a % 1));
    }

    //769 - простое число, находится ровно между 2^9 и 2^10
    public static int ModMethod(int key)
        => key % 769;

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