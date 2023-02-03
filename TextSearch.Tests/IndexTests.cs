namespace TextSearch.Tests;

public class TextSearchIndexTests
{
    [Fact]
    public void FindsNothing()
    {
        var index = CreateIndex();

        var result = index.Search("This is not Latin");

        Assert.Empty(result);
    }

    [Fact]
    public void Error_SearchTextLength()
    {
        var index = CreateIndex();

        Assert.Throws<ArgumentOutOfRangeException>(() => index.Search("a"));
    }

    [Theory]
    [InlineData("Donec ut mauris eget massa tempor convallis", 37)]
    [InlineData("magnis", 54)]
    [InlineData("This is not Latin", 0)]
    public void FindsElements(string searchText, int expectedCount)
    {
        var index = CreateIndex();

        var result = index.Search(searchText);

        Assert.Equal(expectedCount, result.Count());
    }

    private static Index<string> CreateIndex()
    {
        Index<string> index = new(3);

        using StreamReader reader = new("Resources/phrases.csv");

        while (!reader.EndOfStream)
        {
            string? line = reader.ReadLine();

            if (line != null)
            {
                index.Add(line, line);
            }
        }

        return index;
    }
}