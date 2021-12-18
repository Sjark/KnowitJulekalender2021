namespace KnowitJulekalender2021.Dag17;

public class Dag17 : ISolution
{
    private readonly List<char> _sortOrder = new List<char> { ' ', 'A', 'a', 'B', 'b', 'C', 'c', 'D', 'd', 'E', 'e', 'F', 'f', 'G', 'g', 'H', 'h', 'I', 'i', 'J', 'j', 'K', 'k', 'L', 'l', 'M', 'm', 'N', 'n', 'O', 'o', 'P', 'p', 'Q', 'q', 'R', 'r', 'S', 's', 'T', 't', 'U', 'u', 'V', 'v', 'W', 'w', 'X', 'x', 'Y', 'y', 'Z', 'z', 'Æ', 'æ', 'Ø', 'ø', 'Å', 'å' };
    
    public void Execute()
    {
        var elves = File.ReadAllLines($"{AppContext.BaseDirectory}\\Dag17\\alverekke.txt");
        var elvesToRemove = new List<string>();
        var leftCompareIndex = 0;

        for (int i = 1; i < elves.Length; i++)
        {
            var leftCompare = elves[leftCompareIndex];

            if (!IsSorted(leftCompare, elves[i]))
            {
                Console.WriteLine($"{leftCompare} > {elves[i]}");
                elvesToRemove.Add(elves[i]);
            }
            else
            {
                leftCompareIndex = i;
            }
        }

        Console.WriteLine(elves.Sum(x => x.Length) - elvesToRemove.Sum(x => x.Length));
    }

    private bool IsSorted(string a, string b)
    {
        if (a == b) { return true; }

        for (int i = 0; i < a.Length; i++)
        {
            if (i > b.Length - 1)
            {
                return false;
            }

            var aIndex = _sortOrder.IndexOf(a[i]);
            var bIndex = _sortOrder.IndexOf(b[i]);

            if (aIndex > bIndex) {
                return false;
            }
            else if (bIndex > aIndex)
            {
                return true;
            }
        }

        return true;
    }
}
