namespace KnowitJulekalender2021.Dag24;

public class Dag24 : ISolution
{
    public void Execute()
    {
        var feilregistreringer = File.ReadAllLines($"{AppContext.BaseDirectory}\\Dag24\\feilregistreringer.txt")
            .Select(a => new KeyValuePair<string, List<long>>(a.Split("/").First(), a.Split("/").Skip(1).Select(a => long.Parse(a.Trim())).ToList()))
            .ToDictionary(a => a.Key, a => a.Value);

        var skritt = File.ReadAllLines($"{AppContext.BaseDirectory}\\Dag24\\skritt.txt");

        var weeks = new List<Dictionary<Worker, List<long>>>();

        var currentWeek = new Dictionary<Worker, List<long>>();

        foreach (var line in skritt)
        {
            if (line == "##")
            {
                weeks.Add(currentWeek);
                currentWeek = new Dictionary<Worker, List<long>>();
                continue;
            }

            var splittedLine = line.Split("/");
            currentWeek.Add(new Worker(splittedLine[0].Trim(), splittedLine[1].Trim()), splittedLine.Skip(2).Select(a => string.IsNullOrWhiteSpace(a) ? 0L : long.Parse(a.Trim())).ToList());
        }

        if (currentWeek.Count > 0)
        {
            weeks.Add(currentWeek);
        }
    }
}

public record Worker(string Name, string Work);
