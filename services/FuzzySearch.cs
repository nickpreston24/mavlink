namespace mavlink;

public class FuzzySearch
{
    public string first { get; set; } = string.Empty;
    public string second { get; set; } = string.Empty;

    public int distance => first.LevenshteinDistance(second);

    public FuzzySearch(string first, string second)
    {
    }
}