namespace TextSearch.Benchmarks;

using BenchmarkDotNet.Attributes;

public class IndexCreationBenchmark
{
    private const int PhraseCount = 100000;

    private List<string>? phrases;

    private Index<string>? index;

    [IterationSetup]
    public void Setup()
    {
        index = new(3);
        phrases = new();

        Bogus.Faker faker = new();

        for (int i = 0; i < PhraseCount; i++)
        {
            string phrase = faker.Random.Words(faker.Random.Number(1, 20));

            phrases.Add(phrase);
        }
    }

    [Benchmark]
    public void Fill()
    {
        foreach (var phrase in phrases!)
        {
            index!.Add(phrase, phrase);
        }
    }
}
