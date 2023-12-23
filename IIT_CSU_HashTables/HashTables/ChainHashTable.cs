using IIT_CSU_HashTables.Collections;

namespace IIT_CSU_HashTables.HashTables;

public class ChainHashTable<TValue>(HashFunction hashFunction, int capacity = 1000)
{
    private readonly MyLinkedList<KeyValuePair<int, TValue>>?[] _values =
        new MyLinkedList<KeyValuePair<int, TValue>>?[capacity];
    public int Capacity { get; private set; } = capacity;
    public int Count { get; private set; }

    /// <summary>
    /// Метод для добавления пары ключ-значение в хэш-таблицу
    /// </summary>
    /// <param name="key">Ключ</param>
    /// <param name="value">Значение</param>
    /// <returns>true - если элемент удалось вставить.
    /// false - если не получилось вставить (пара с таким ключом уже есть в таблице)</returns>
    public bool Add(int key, TValue value)
    {
        int hashKey = hashFunction(key);
        if (_values[hashKey] is null)
        {
            _values[hashKey] = new MyLinkedList<KeyValuePair<int, TValue>>();
            Count++;
        }
        else
        {
            if (_values[hashKey]!.Contains(pair => Equals(pair.Key, key)))
            {
                return false;
            }
        }
        
        _values[hashKey]!.AppendFirst(new KeyValuePair<int, TValue>(key, value));
        return true;
    }

    public void Remove(KeyValuePair<int, TValue> pair)
    {
        int hashKey = hashFunction(pair.Key);
        if (_values[hashKey] is null)
        {
            throw new KeyNotFoundException("В таблице нет ключа с таким значением.");
        }
            
        var value = _values[hashKey]!.First(p => p.Key == pair.Key);
        _values[hashKey]!.Remove(value);
        if (_values[hashKey]!.Count == 0)
        {
            _values[hashKey] = null;
        }
    }

    public TValue this[int key]
    {
        get
        {
            int hashKey = hashFunction(key);
            if (_values[hashKey] is null)
            {
                throw new KeyNotFoundException("В таблице нет ключа с таким значением.");
            }
            
            return _values[hashKey]!.First(pair => Equals(pair.Key, key)).Value;
        }
    }

    public double GetFillFactor()
        => (double)Count / Capacity;

    public int GetLengthOfTheLongestChain()
    {
        int maximum = 0;
        foreach (var list in _values)
        {
            if (list is null)
            {
                continue;
            }

            if (list.Count >= maximum)
            {
                maximum = list.Count;
            }
        }

        return maximum;
    }

    public int GetLengthOfTheShortestChain()
    {
        int minimum = int.MaxValue;
        foreach (var list in _values)
        {
            if (list is null)
            {
                continue;
            }

            if (list.Count <= minimum)
            {
                minimum = list.Count;
            }
        }

        return minimum;
    }
}