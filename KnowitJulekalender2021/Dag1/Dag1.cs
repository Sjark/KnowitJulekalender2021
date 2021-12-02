namespace KnowitJulekalender2021.Dag1;

public class Dag1 : ISolution
{
    private readonly Dictionary<string, int> _dayTranslation = new Dictionary<string, int>()
    {
        { "femti", 50 },
        { "førti", 40 },
        { "tretti", 30 },
        { "tjue", 20 },
        { "nitten", 19 },
        { "atten", 18 },
        { "sytten", 17 },
        { "seksten", 16 },
        { "femten", 15 },
        { "fjorten", 14 },
        { "tretten", 13 },
        { "tolv", 12 },
        { "elleve", 11 },
        { "ti", 10 },
        { "ni", 9 },
        { "åtte", 8 },
        { "sju", 7 },
        { "seks", 6 },
        { "fem", 5 },
        { "fire", 4 },
        { "tre", 3 },
        { "to", 2 },
        { "en", 1 }
    };

    public void Execute()
    {
        var numbers = File.ReadAllText($"{AppContext.BaseDirectory}\\Dag1\\tall.txt").Replace("\n", "").AsSpan();

        var pointer = 0;
        ReadOnlySpan<char> currentNumbers;
        var translationKeys = _dayTranslation.Keys;
        var sum = 0;

        while (pointer < numbers.Length)
        {
            currentNumbers = numbers.Slice(pointer);

            foreach (var key in translationKeys)
            {
                if (currentNumbers.StartsWith(key))
                {
                    sum += _dayTranslation[key];
                    pointer += key.Length;
                    break;
                }
            }
        }

        Console.WriteLine(sum);
    }
}
