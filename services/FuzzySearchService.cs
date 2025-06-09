using CodeMechanic.Async;
using CodeMechanic.Shargs;

namespace mavlink;

public class FuzzySearchService : QueuedService
{
    private readonly ArgsMap arguments;

    public FuzzySearchService(ArgsMap arguments)
    {
        this.arguments = arguments;
        if (arguments.HasFlag("--search"))
        {
            steps.Add(Search);
        }
    }

    public async Task Search()
    {
        (_, string search_term) = arguments.WithFlags("--search");

        Console.WriteLine($"searching for '{search_term}'");
        // Console.WriteLine(nameof(Search));
        List<string[]> l = new List<string[]>
        {
            new string[] { "ant", search_term },
            new string[] { "Sam", search_term }
        };

        foreach (string[] a in l)
        {
            int cost = a[0].LevenshteinDistance(a[1]);
            Console.WriteLine("{0} -> {1} = {2}", a[0], a[1], cost);
        }
    }
}