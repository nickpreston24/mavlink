using CodeMechanic.Async;
using CodeMechanic.FileSystem;
using CodeMechanic.Shargs;
using Vogen;

namespace mavlink;

public class FuzzySearchService : QueuedService
{
    private readonly ArgsMap arguments;
    private readonly Mode mode;

    public FuzzySearchService(ArgsMap arguments)
    {
        this.arguments = arguments;

        if (this.arguments.HasCommand("search"))
        {
            mode = Mode.Interactive;

            //todo: finish this mode.
        }

        if (arguments.HasFlag("--search"))
        {
            steps.Add(Search);
        }

        if (this.arguments.HasCommand("pull"))
        {
            if (Directory.Exists(MavlinkConsts.dotnet_folder.Value))
            {
                string filepath = Path.Combine(
                    MavlinkConsts.dotnet_folder.Value,
                    "fubar.txt");

                // File.WriteAllText(filepath, "foo bar");
                new SaveFile("foo bar")
                    .To(MavlinkConsts.dotnet_folder.Value)
                    .As("fubar.txt");
            }
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

[ValueObject<string>]
[Instance("repo", "https://github.com/mavlink/mavlink-devguide.git")]
[Instance("local_folder", "mavlink-devguide")]
[Instance("dotnet_folder",
    $"~/.dotnet/tools/{nameof(mavlink)}/.mavlink-devguide")]
public partial class MavlinkConsts
{
}

[ValueObject<string>]
[Instance("Interactive", "interactive")]
public partial class Mode;