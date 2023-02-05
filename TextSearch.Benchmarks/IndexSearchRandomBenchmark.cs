namespace TextSearch.Benchmarks;

using BenchmarkDotNet.Attributes;

public class IndexSearchRandomBenchmark
{
    private const int PhraseCount = 100000;

    private readonly List<string> phrases;
    private readonly Index<string> index;

    [Params(3, 5, 20)]
    public int? SearchTextLength { get; set; }

    public IndexSearchRandomBenchmark()
    {
        phrases = new();
        index = new(3);

        Bogus.Faker faker = new();

        for (int i = 0; i < PhraseCount; i++)
        {
            string phrase = faker.Random.Words(faker.Random.Number(1, 20));

            phrases.Add(phrase);
            index.Add(phrase, phrase);
        }
    }

    [Benchmark(Baseline = true)]
    public int SearchWithLinq()
    {
        var searchText = SearchText;
        return phrases.Where(p => p.Contains(searchText)).Count();
    }

    [Benchmark]
    public int Search()
    {
        return index.Search(SearchText).Count();
    }

    private string SearchText => phrases.Where(p => p.Length >= SearchTextLength).First()[..(int)SearchTextLength!];
}