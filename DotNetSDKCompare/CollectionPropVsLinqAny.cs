using System.Collections.ObjectModel;
using BenchmarkDotNet.Attributes;

namespace DotNetSDKCompare;

[MemoryDiagnoser(displayGenColumns: false)]
[HideColumns("StdDev", "Median", "Job", "RatioSD", "Error", "Alloc Ratio")]
[ExecutionValidator(failOnError: true)]
[ReturnValueValidator]
public class CollectionPropVsLinqAny
{
    [Params(10, 100, 1000, 1000_000)]
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public int CollectionSize { get; set; }

    private int[] _array = null!;
    private List<int> _list = null!;
    private SortedList<int, string> _sortedList = null!;
    private LinkedList<int> _linkedList = null!;
    private HashSet<int> _hashSet = null!;
    private SortedList<int, string> _sortedSet = null!;
    private Dictionary<int, string> _dictionary = null!;
    private Queue<int> _queue = null!;
    private Stack<int> _stack = null!;
    private ObservableCollection<int> _observableCollection = null!;

    [GlobalSetup]
    public void InitCollections()
    {
        _array = new int[CollectionSize];
        _list = new List<int>(CollectionSize);
        _sortedList = new SortedList<int, string>(CollectionSize);
        _linkedList = [];
        _hashSet = [];
        _sortedSet = new SortedList<int, string>();
        _dictionary = new Dictionary<int, string>();
        _queue = new Queue<int>(CollectionSize);
        _stack = new Stack<int>(CollectionSize);
        _observableCollection = [];

        for (var i = 0; i < CollectionSize; i++)
        {
            _array[i] = i;
            _list.Add(i);
            _sortedList.Add(i, i.ToString());
            _linkedList.AddLast(i);
            _hashSet.Add(i);
            _sortedSet.Add(i, i.ToString());
            _dictionary.Add(i, i.ToString());
            _queue.Enqueue(i);
            _stack.Push(i);
            _observableCollection.Add(i);
        }
    }

    #region LINQ Any() benchmarks

    [Benchmark(Description = "Array Any()")]
    [BenchmarkCategory("int[]")]
    public bool ArrayAny() => _array.Any();

    [Benchmark(Description = "List Any()")]
    [BenchmarkCategory("List<int>")]
    public bool ListAny() => _list.Any();

    [Benchmark(Description = "SortedList Any()")]
    [BenchmarkCategory("SortedList<int, string>")]
    public bool SortedListAny() => _sortedList.Any();

    [Benchmark(Description = "LinkedList Any()")]
    [BenchmarkCategory("LinkedList<int>")]
    public bool LinkedListAny() => _linkedList.Any();

    [Benchmark(Description = "HashSet Any()")]
    [BenchmarkCategory("HashSet<int>")]
    public bool HashSetAny() => _hashSet.Any();

    [Benchmark(Description = "SortedSet Any()")]
    [BenchmarkCategory("SortedSet<int>")]
    public bool SortedSetAny() => _sortedSet.Any();

    [Benchmark(Description = "Dictionary Any()")]
    [BenchmarkCategory("Dictionary<int, string>")]
    public bool DictionaryAny() => _dictionary.Any();

    [Benchmark(Description = "Queue Any()")]
    [BenchmarkCategory("Queue<int>")]
    public bool QueueAny() => _queue.Any();

    [Benchmark(Description = "Stack Any()")]
    [BenchmarkCategory("Stack<int>")]
    public bool StackAny() => _stack.Any();

    [Benchmark(Description = "ObservableCollection Any()")]
    [BenchmarkCategory("ObservableCollection<int>")]
    public bool ObservableCollectionAny() => _observableCollection.Any();

    #endregion

    #region Built-in Property benchmarks

    [Benchmark(Description = "Array Length > 0")]
    [BenchmarkCategory("int[]")]
    public bool ArrayLength() => _array.Length > 0;

    [Benchmark(Description = "List Count > 0")]
    [BenchmarkCategory("List<int>")]
    public bool ListCount() => _list.Count > 0;

    [Benchmark(Description = "SortedList Count > 0")]
    [BenchmarkCategory("SortedList<int, string>")]
    public bool SortedListCount() => _sortedList.Count > 0;

    [Benchmark(Description = "LinkedList Count > 0")]
    [BenchmarkCategory("LinkedList<int>")]
    public bool LinkedListCount() => _linkedList.Count > 0;

    [Benchmark(Description = "HashSet Count > 0")]
    [BenchmarkCategory("HashSet<int>")]
    public bool HashSetCount() => _hashSet.Count > 0;

    [Benchmark(Description = "SortedSet Count > 0")]
    [BenchmarkCategory("SortedSet<int>")]
    public bool SortedSetCount() => _sortedSet.Count > 0;

    [Benchmark(Description = "Dictionary Count > 0")]
    [BenchmarkCategory("Dictionary<int, string>")]
    public bool DictionaryCount() => _dictionary.Count > 0;

    [Benchmark(Description = "Queue Count > 0")]
    [BenchmarkCategory("Queue<int>")]
    public bool QueueCount() => _queue.Count > 0;

    [Benchmark(Description = "Stack Count > 0")]
    [BenchmarkCategory("Stack<int>")]
    public bool StackCount() => _stack.Count > 0;

    [Benchmark(Description = "ObservableCollection Count > 0")]
    [BenchmarkCategory("ObservableCollection<int>")]
    public bool ObservableCollectionCount() => _observableCollection.Count > 0;

    #endregion
}