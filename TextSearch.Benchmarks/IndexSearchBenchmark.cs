namespace TextSearch.Benchmarks;

using BenchmarkDotNet.Attributes;

public class IndexSearchBenchmark
{
    private readonly List<string> phrases;
    private readonly Index<string> index;

    [Params("Aenean", "nis", "facilisi")]
    public string? SearchText { get; set; }

    public IndexSearchBenchmark()
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

    [Benchmark(Baseline = true)]
    public int SearchWithLinq()
    {
        return phrases.Where(p => p.Contains(SearchText!)).Count();
    }

    [Benchmark]
    public int Search()
    {
        return index.Search(SearchText!).Count();
    }
}