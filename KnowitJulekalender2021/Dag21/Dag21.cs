using System.Text;

namespace KnowitJulekalender2021.Dag21;

public class Dag21 : ISolution
{
    public void Execute()
    {
        var dict = new Dictionary<int, char>
        {
            { 1, 'a' },
            { 2, 'b' },
            { 3, 'c' },
            { 4, 'd' },
            { 5, 'e' },
            { 6, 'f' },
            { 7, 'g' },
            { 8, 'h' },
            { 9, 'i' },
            { 10, 'j' },
            { 11, 'k' },
            { 12, 'l' },
            { 13, 'm' },
            { 14, 'n' },
            { 15, 'o' },
            { 16, 'p' },
            { 17, 'q' },
            { 18, 'r' },
            { 19, 's' },
            { 20, 't' },
            { 21, 'u' },
            { 22, 'v' },
            { 23, 'w' },
            { 24, 'x' },
            { 25, 'y' },
            { 26, 'z' },
            { 27, 'æ' },
            { 28, 'ø' },
            { 29, 'å' }
        };

        var encrypted = "45205145192051057281419115181357209121021125181201516161911252091475141221011351923522729182181222718192919149121210211251491919514";

        var possibleWords = new List<string>();
        var substitutions = 0;
        var moreToSubstitute = true;

        while (moreToSubstitute)
        {
            var word = new StringBuilder();
            var currentSubstitutions = 0;

            for (int i = 0; i < encrypted.Length; i++)
            {
                if (encrypted[i] == '0')
                {
                    continue;
                }
                
                if (currentSubstitutions != substitutions && i < encrypted.Length - 1 && int.Parse($"{encrypted[i]}{encrypted[i + 1]}") < 30)
                {
                    word.Append(dict[int.Parse($"{encrypted[i]}{encrypted[i + 1]}")]);
                    i++;
                    currentSubstitutions++;
                }
                else
                {
                    word.Append(dict[int.Parse($"{encrypted[i]}")]);
                }
            }

            possibleWords.Add(word.ToString());

            if (currentSubstitutions < substitutions)
            {
                moreToSubstitute = false;
            }
            else
            {
                substitutions++;
            }
        }

        foreach (var word in possibleWords)
        {
            Console.WriteLine(word);
        }
    }
}
