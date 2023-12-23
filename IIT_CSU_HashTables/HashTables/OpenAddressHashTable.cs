using IIT_CSU_HashTables.Exceptions;
using IIT_CSU_HashTables.HashFunctions;

namespace IIT_CSU_HashTables.HashTables;

public class OpenAddressHashTable<TValue>(BiHashFunction hashFunction, int capacity = 10_000)
{
    private const int MaxProbes = 7;
    private readonly KeyValuePair<int, TValue>?[] _values = new KeyValuePair<int, TValue>?[capacity];

    public int Capacity { get; private set; } = capacity;
    public int Count { get; private set; }

    public void Add(int key, TValue value)
    {
        for (int i = 0; i < MaxProbes; i++)
        {
            int hashKey = hashFunction(key, i) % Capacity;
            if (_values[hashKey] is not null) continue;
            _values[hashKey] = new KeyValuePair<int, TValue>(key, value);
            Count++;
        }

        throw new TableOverflowException();
    }

    public void RemoveByKey(int key)
    {
        for (int i = 0; i < MaxProbes; i++)
        {
            int hashKey = hashFunction(key, i) % Capacity;
            var pair = _values[hashKey];
            if (pair is null)
            {
                break;
            }

            if (pair.Value.Key != key)
            {
                continue;
            }

            _values[hashKey] = null;
            return;
        }
        
        throw new KeyNotFoundException();
    }

    public TValue this[int key]
    {
        get
        {
            for (int i = 0; i < MaxProbes; i++)
            {
                int hashKey = hashFunction(key, i) % Capacity;
                var pair = _values[hashKey];
                if (pair is null)
                {
                    break;
                }

                if (pair.Value.Key == key)
                {
                    return pair.Value.Value;
                }
            }

            throw new KeyNotFoundException();
        }
    }

    public int GetMaxClusterLength()
    {
        int maxCluster = 0;
        int currentCluster = 0;
        int i = 0;
        while (i < Capacity)
        {
            if (_values[i] is not null)
            {
                currentCluster++;
            }
            else
            {
                maxCluster = int.Max(maxCluster, currentCluster);
                currentCluster = 0;
            }
        }

        return int.Max(maxCluster, currentCluster);
    }
}