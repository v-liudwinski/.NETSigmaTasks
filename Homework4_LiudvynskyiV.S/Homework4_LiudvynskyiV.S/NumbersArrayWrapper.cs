namespace Homework4_LiudvynskyiV.S;

public class NumbersArrayWrapper
{
    private readonly int _length;
    private int[] _array;
    
    public NumbersArrayWrapper(int length)
    {
        _length = length;
        FulfillArray();
    }

    public NumbersArrayWrapper()
    {
        _length = 10;
        FulfillArray();
    }
    
    public int this[int index]
    {
        get => _array[index];
        set => _array[index] = value;
    }

    public int[] GetArray() => _array;
    
    private void FulfillArray()
    {
        _array = new int[_length];
        var random = new Random();
        for (var i = 0; i < _length; i++)
        {
            _array[i] = random.Next(0, 100);
        }
    }

    public string GetUniqueElementsAndTheirQnt()
    {
        var uniqueElements = _array.Distinct().Select(x =>
        {
            var qnt = _array.Count(y => y == x);
            return $"{x}: {qnt}";
        });
        return string.Join("\n", uniqueElements);
    }

    public void PrintTwoLongestPrimeSequences()
    {
        var sequences = new List<List<int>>();
        
        for (var i = 0; i < _length; i++)
        {
            if (IsPrime(_array[i]))
            {
                var primeNums = new List<int>();
                for (int j = i; j < _length; j++)
                {
                    if (IsPrime(_array[j]))
                    {
                        primeNums.Add(_array[j]);
                        if (j == _length - 1)
                        {
                            sequences.Add(primeNums);
                        }
                    }
                    else
                    {
                        i = j;
                        sequences.Add(primeNums);
                        break;
                    }
                }
            }
        }

        var sortedSequences = sequences
            .OrderByDescending(x => x.Count)
            .Take(2)
            .ToList();
        
        sortedSequences.ForEach(x =>
        {
            foreach (var n in x)
            {
                Console.Write($"{n} ");
            }
            Console.WriteLine();
        });
    }

    private bool IsPrime(int n)
    {
        if (n > 1)
        {
            return Enumerable.Range(1, n).Where(x => n%x == 0)
                .SequenceEqual(new[] {1, n});
        }

        return false;
    }
}