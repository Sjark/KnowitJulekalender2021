namespace KnowitJulekalender2021.Dag11;

public class Dag11 : ISolution
{
    public void Execute()
    {
        var names = File.ReadAllLines($"{AppContext.BaseDirectory}\\Dag11\\names.txt");

        var namesWithErrors = new Dictionary<string, List<string>>();
        var gifts = new Dictionary<string, int>();

        foreach (var name in names)
        {
            namesWithErrors.Add(name, new List<string> { name });
            gifts.Add(name, 0);

            for (int i = 0; i < name.Length - 1; i++)
            {
                var tempName = name.ToCharArray();
                var secondChar = name[i + 1];

                tempName[i + 1] = tempName[i];
                tempName[i] = secondChar;

                namesWithErrors[name].Add(new string(tempName));
            }
        }

        var locked = File.ReadAllLines($"{AppContext.BaseDirectory}\\Dag11\\locked.txt");

        foreach (var phrase in locked)
        {
            string? nameFound = null;
            var excessCharacters = int.MaxValue;
            var excessCharactersSet = new HashSet<int>();

            foreach (var name in namesWithErrors.Keys)
            {
                var match = IsMatch(phrase, namesWithErrors[name]);

                if (match.Found)
                {
                    if (excessCharactersSet.Contains(match.ExcessCharacters) && match.ExcessCharacters == excessCharacters)
                    {
                        nameFound = null;
                        continue;
                    }

                    excessCharactersSet.Add(match.ExcessCharacters);

                    if (match.ExcessCharacters < excessCharacters)
                    {
                        nameFound = name;
                        excessCharacters = match.ExcessCharacters;
                    }
                }
            }

            if (nameFound != null)
            {
                gifts[nameFound]++;
            }
        }

        var bestKid = gifts.OrderByDescending(a => a.Value).First();

        Console.WriteLine($"{bestKid.Key},{bestKid.Value}");
    }

    public (bool Found, int ExcessCharacters) IsMatch(string phrase, List<string> names)
    {
        bool found = false;
        var excessCharacters = int.MaxValue;
        foreach (var name in names)
        {
            var currentChar = 0;
            var firstCharIndex = -1;
            var lastCharIndex = -1;

            for (var i = 0; i < phrase.Length; i++)
            {
                if (phrase[i] == name[currentChar])
                {
                    currentChar++;

                    if (firstCharIndex == -1)
                    {
                        firstCharIndex = i;
                    }

                    if (currentChar == name.Length)
                    {
                        lastCharIndex = i;
                        break;
                    }
                }
            }

            if (lastCharIndex != -1)
            {
                var tempExcess = lastCharIndex - firstCharIndex - name.Length + 1;

                if (tempExcess < excessCharacters)
                {
                    excessCharacters = tempExcess;
                    found = true;
                }
            }
        }

        return (found, excessCharacters);
    }
}
