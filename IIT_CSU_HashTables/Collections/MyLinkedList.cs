using System.Collections;

namespace IIT_CSU_HashTables.Collections;

public class MyLinkedList<T> : IEnumerable<T>
{
    public class MyNode(T value, MyNode? next = null)
    {
        public T Value = value;
        public MyNode? Next = next;
    }

    private MyNode? _head;
    private MyNode? _tail;

    public int Count { get; set; }

    public bool IsEmpty => Count == 0;

    public void Add(T value)
    {
        var newNode = new MyNode(value);
        if (_head is null)
        {
            _head = newNode;
        }
        else
        {
            _tail!.Next = newNode;
        }

        _tail = newNode;
        Count++;
    }

    public void InsertAt(int index, T value)
    {
        if (index < 0 || index >= Count)
        {
            throw new ArgumentException("Index is out of bounds of the list");
        }

        var newNode = new MyNode(value);
        var node = _head;
        for (var i = 0; i < index - 1; i++)
        {
            node = node?.Next;
        }

        var next = node?.Next;
        node!.Next = newNode;
        newNode.Next = next;
        Count++;
    }

    public T? Remove(T value)
    {
        var current = _head;
        MyNode? previous = null;
        while (current!.Value is not null)
        {
            if (current.Value.Equals(value))
            {
                if (previous is not null)
                {
                    previous.Next = current.Next;
                    if (current.Next is null)
                    {
                        _tail = previous;
                    }
                }
                else
                {
                    _head = _head?.Next;
                    if (_head is null)
                    {
                        _tail = null;
                    }
                }

                Count--;
                return current.Value;
            }

            previous = current;
            current = current.Next;
        }

        throw new Exception();
    }

    public bool Contains(T value)
    {
        var current = _head;
        if (Count == 0)
        {
            return false;
        }

        while (current is not null)
        {
            if (current.Value!.Equals(value))
            {
                return true;
            }

            current = current.Next;
        }

        return false;
    }

    public bool Contains(Predicate<T> predicate)
    {
        var current = _head;
        if (Count == 0)
        {
            return false;
        }

        while (current is not null)
        {
            if (predicate(current.Value))
            {
                return true;
            }

            current = current.Next;
        }

        return false;
    }

    public void Clear()
    {
        _head = null;
        _tail = null;
        Count = 0;
    }

    public void AppendFirst(T value)
    {
        var node = new MyNode(value)
        {
            Next = _head
        };
        _head = node;
        if (Count == 0)
        {
            _tail = _head;
        }

        Count++;
    }

    public IEnumerator<T> GetEnumerator()
    {
        var current = _head;
        while (current is not null)
        {
            yield return current.Value;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();

    public T this[int index]
    {
        get
        {
            var result = _head;
            for (int i = 0; i < index; i++)
            {
                result = result?.Next;
            }

            return result!.Value;
        }
        set
        {
            if (index == Count)
            {
                Add(value);
            }
            else if (index == 0)
            {
                _head = new MyNode(value, _head?.Next);
            }
            else
            {
                var node = _head!;
                for (int i = 0; i < index - 1; i++)
                {
                    if (node.Next is null)
                    {
                        break;
                    }

                    node = node.Next;
                }

                var temp = node.Next!.Next;
                node.Next = new MyNode(value, temp);
            }
        }
    }
}