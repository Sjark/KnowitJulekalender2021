using System.Text;

namespace KnowitJulekalender2021.Dag24;

public class Dag24 : ISolution
{
    public void Execute()
    {
        var feilregistreringer = File.ReadAllLines($"{AppContext.BaseDirectory}\\Dag24\\feilregistreringer.txt")
            .Select(a => new KeyValuePair<string, List<long>>(a.Split("/").First().Trim(), a.Split("/").Skip(1).Select(a => long.Parse(a.Trim())).ToList()))
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

        foreach (var week in weeks)
        {
            foreach (var worker in week)
            {
                var feil = feilregistreringer[worker.Key.Work];

                if (feil != null)
                {
                    for (int i = 0; i < worker.Value.Count; i++)
                    {
                        if (worker.Value[i] != 0)
                        {
                            worker.Value[i] -= feil[i];
                        }
                        if (worker.Value[i] < 0)
                        {
                            worker.Value[i] = 0;
                        }
                    }
                }
            }
        }

        var nissen = weeks.Select(a => a.Where(b => b.Key.Name == "Nissen 🎅").Single().Value)
            .ToList();

        var alvene = weeks.Select(a => a.Where(b => b.Key.Name != "Nissen 🎅")).ToList();

        var nissenAvg = new List<double>();

        for (int i = 0; i < 7; i++)
        {
            nissenAvg.Add((double)nissen.Select(a => a[i]).Sum() / nissen.Count);
        }

        var alveneAvg = new List<long>() { 0, 0, 0, 0, 0, 0, 0 };

        foreach (var alv in alvene)
        {
            for (int i = 0; i < 7; i++)
            {
                alveneAvg[i] += alv.Select(a => a.Value[i]).Sum();
            }
        }

        var result = new StringBuilder();

        for (int i = 0; i < 7; i++)
        {
            result.Append(Math.Floor(nissenAvg[i] - ((double)alveneAvg[i] / weeks.Count / alvene.First().Count())));
        }

        Console.WriteLine(result.ToString());
    }
}

public record Worker(string Name, string Work);
