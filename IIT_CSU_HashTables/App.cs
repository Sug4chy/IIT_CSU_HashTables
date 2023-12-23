using IIT_CSU_HashTables.HashTables;

namespace IIT_CSU_HashTables;

public static class App
{
    public static void Start()
    {
        Console.WriteLine();
        Console.WriteLine("Лабораторная работа №6. Хэш-таблицы");
        Console.WriteLine("Выберите, с каким методом разрешения коллизий хотите ознакомиться:");
        Console.WriteLine("1. Метод цепочек");
        Console.WriteLine("2. Метод открытой адрессации");
        Console.Write("Введите число: ");
        string? input = Console.ReadLine();
        int i;
        try
        {
            i = int.Parse(input);
        }
        catch (Exception)
        {
            Console.Clear();
            Console.WriteLine("Пожалуйста, введите число.");
            Thread.Sleep(TimeSpan.FromSeconds(3));
            Console.Clear();
            Start();
            return;
        }

        switch (i)
        {
            case 1:
                HandleChainHashTable();
                break;
            case 2:
                HandleOpenAddrHashTable();
                break;
            default:
                Console.Clear();
                Console.WriteLine("Пожалуйста, введите только 1 или 2.");
                Thread.Sleep(TimeSpan.FromSeconds(3));
                Console.Clear();
                Start();
                return;
        }
    }

    private static void HandleChainHashTable()
    {
        Console.WriteLine();
        Console.WriteLine("Генерируем 100.000 пар...");
        var pairs = PairsGenerator.Generate(100_000);
        var keyValuePairs = pairs as KeyValuePair<int, string>[] ?? pairs.ToArray();
        
        var ht1 = new ChainHashTable<string>(HashFunctions.HashFunctions.StdCombine);
        ht1.AddRange(keyValuePairs);
        Console.WriteLine("Добавили все пары в таблицу с хэш-функцией из стандартной библиотеки .NET");
        
        var ht2 = new ChainHashTable<string>(HashFunctions.HashFunctions.ModMethod);
        ht2.AddRange(keyValuePairs);
        Console.WriteLine("Добавили все пары в таблицу с хэш-функцией по методу деления");
        
        var ht3 = new ChainHashTable<string>(HashFunctions.HashFunctions.DigitsSum);
        ht3.AddRange(keyValuePairs);
        Console.WriteLine("Добавили все пары в таблицу с хэш-функцией, которая суммирует цифры числа");
        
        var ht4 = new ChainHashTable<string>(HashFunctions.HashFunctions.MultiplyMethod);
        ht4.AddRange(keyValuePairs);
        Console.WriteLine("Добавили все пары в таблицу с хэш-функцией по методу умножения");
        
        var ht5 = new ChainHashTable<string>(HashFunctions.HashFunctions.TwoPowTen);
        ht5.AddRange(keyValuePairs);
        Console.WriteLine("Добавили все пары в таблицу с самописной хэш-функцией");

        var ht = new List<ChainHashTable<string>>
        {
            ht1,
            ht2,
            ht3,
            ht4,
            ht5
        };
        string[] names = 
        {
            "Хэш-функция из std .NET", 
            "Метод деления", 
            "Сумма цифр числа", 
            "Метод умножения", 
            "Самописная хэш-функция"
        };
        int i = 0;

        foreach (var table in ht)
        {
            Console.WriteLine();
            Console.WriteLine(names[i]);
            i++;
            Console.WriteLine($"Коэффициент заполнения: {table.GetFillFactor()}");
            Console.WriteLine($"Длина самой длинной цепочки: {table.GetLengthOfTheLongestChain()}");
            Console.WriteLine($"Длина самой короткой цепочки: {table.GetLengthOfTheShortestChain()}");
        }
    }

    private static void HandleOpenAddrHashTable()
    {
        Console.WriteLine();
        Console.WriteLine("Генерируем 10.000 пар...");
        var pairs = PairsGenerator.Generate(10_000);
        var keyValuePairs = pairs as KeyValuePair<int, string>[] ?? pairs.ToArray();
        
        var ht1 = new OpenAddressHashTable<string>(HashFunctions.BiHashFunctions.LinearProbing);
        ht1.AddRange(keyValuePairs);
        Console.WriteLine("Добавили все пары в таблицу с линейным пробированием");
        
        var ht2 = new OpenAddressHashTable<string>(HashFunctions.BiHashFunctions.QuadraticProbing);
        ht2.AddRange(keyValuePairs);
        Console.WriteLine("Добавили все пары в таблицу с квадратичным пробированием");

        var ht3 = new OpenAddressHashTable<string>(HashFunctions.BiHashFunctions.DoubleHashing);
        ht3.AddRange(keyValuePairs);
        Console.WriteLine("Добавили все пары в таблицу с двойным хэшированием (функция из std + метод деления)");
        
        var ht4 = new OpenAddressHashTable<string>(HashFunctions.BiHashFunctions.XorMethod);ht4.AddRange(keyValuePairs);
        Console.WriteLine("Добавили все пары в таблицу с XOR-пробированием");

        var ht5 = new OpenAddressHashTable<string>(HashFunctions.BiHashFunctions.MidSquareMethod);
        ht5.AddRange(keyValuePairs);
        Console.WriteLine("Добавили все пары в таблицу с пробированием методом средних квадратов");

        var ht = new List<OpenAddressHashTable<string>>
        {
            ht1,
            ht2,
            ht3,
            ht4,
            ht5
        };
        string[] names = 
        {
            "Линейное пробирование ", 
            "Квадратичное пробирование", 
            "Двойное хэширование", 
            "XOR-пробирование", 
            "Метод средних квадратов"
        };
        int i = 0;

        foreach (var table in ht)
        {
            Console.WriteLine();
            Console.WriteLine($"{names[i]} (удалось вставить {table.Count} элементов)");
            i++;
            Console.WriteLine($"Длина самого большого кластера: {table.GetMaxClusterLength()}");
        }
    }
}