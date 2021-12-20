namespace KnowitJulekalender2021.Dag19;

public class Dag19 : ISolution
{
    public void Execute()
    {
        var factory = File.ReadAllLines($"{AppContext.BaseDirectory}\\Dag19\\factory.txt");
        var timings = new List<Timing>();

        foreach (var machine in factory.Select(a => a.Split(',')))
        {
            var startTime = int.Parse(machine.First().Split(':').First()) * 60 + int.Parse(machine.First().Split(':').Last());

            for (int i = 1; i < machine.Length - 2; i += 3)
            {
                var prodTime = int.Parse(machine[i + 1]);
                var packageTime = int.Parse(machine[i + 2]);

                startTime += prodTime;
                timings.Add(new Timing(startTime));
                timings.Add(new Timing(startTime + packageTime, true));
            }
        }

        var maxSim = 0;
        var currentSim = 0;

        foreach (var time in timings.OrderBy(a => a.Time).ThenByDescending(a => a.IsEnd))
        {
            if (time.IsEnd)
            {
                currentSim--;
            }
            else
            {
                currentSim++;
            }

            if (currentSim > maxSim)
            {
                maxSim = currentSim;
            }
        }

        Console.WriteLine(maxSim);
    }
}

public record Timing(int Time, bool IsEnd = false);
