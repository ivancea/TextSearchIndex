namespace TextSearch.Benchmarks;

using BenchmarkDotNet.Attributes;

public class IndexBenchmark
{
    private const int N = 1000;

    private readonly List<string> phrases;
    private readonly Index<string> index;

    [Params("Aenean", "nis", "facilisi")]
    public string? SearchText { get; set; }

    public IndexBenchmark()
    {
        phrases = new();
        index = new(3);

        using StreamReader reader = new("Resources/phrases.csv");

        while (!reader.EndOfStream)
        {
            string? line = reader.ReadLine();

            if (line != null)
            {
                phrases.Add(line);
                index.Add(line, line);
            }
        }
    }

    [Benchmark]
    public int SearchWithFind()
    {
        return phrases.Where(p => p.Contains(SearchText!)).Count();
    }

    [Benchmark]
    public int Search()
    {
        return index.Search(SearchText!).Count();
    }
}