namespace TextSearch;

public class Index<T>
{
    public int BlockSize { get; init; }

    public Dictionary<string, List<T>> Elements { get; } = new();

    public Dictionary<string, HashSet<string>> Blocks { get; } = new();

    public Index(int blockSize)
    {
        if (blockSize < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(blockSize), blockSize, "Block size must be greater than zero.");
        }

        BlockSize = blockSize;
    }

    public void Add(string text, T element)
    {
        if (!Elements.ContainsKey(text))
        {
            Elements[text] = new();
        }

        Elements[text].Add(element);

        for (int i = 0; i < text.Length - BlockSize + 1; i++)
        {
            string block = text.Substring(i, BlockSize);

            if (!Blocks.ContainsKey(block))
            {
                Blocks[block] = new();
            }

            Blocks[block].Add(text);
        }
    }

    public IEnumerable<T> Search(string searchText)
    {
        if (searchText.Length < BlockSize)
        {
            throw new ArgumentOutOfRangeException(nameof(searchText), searchText, $"Search text must be at least {BlockSize} characters long.");
        }

        HashSet<string> checkedBlocks = new();
        HashSet<string>? possibleTexts = null;

        for (int i = 0; i < searchText.Length - BlockSize + 1; i++)
        {
            var block = searchText.Substring(i, BlockSize);

            // Check to don't intersect the same block twice
            if (!checkedBlocks.Add(block))
            {
                continue;
            }

            if (!Blocks.TryGetValue(block, out var texts))
            {
                return Enumerable.Empty<T>();
            }

            if (possibleTexts == null)
            {
                possibleTexts = texts;
            }
            else
            {
                possibleTexts.IntersectWith(texts);
            }
        }

        if (possibleTexts == null)
        {
            return Enumerable.Empty<T>();
        }

        return possibleTexts.Where(Elements.ContainsKey).SelectMany(text => Elements[text]);
    }
}
