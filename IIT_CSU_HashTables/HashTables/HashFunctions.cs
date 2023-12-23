namespace IIT_CSU_HashTables.HashTables;

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

    public static int EMethod(int key)
    {
        int result = 0;
        string str = key.ToString();
        for (int i = 0; i < str.Length; i++)
        {
            result += str[i];
        }

        while (result >= 1000)
        {
            result %= (int)Math.Pow(Math.E, result % 10);
        }

        return result;
    }
}

public delegate int HashFunction(int key);