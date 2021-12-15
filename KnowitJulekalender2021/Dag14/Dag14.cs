using System.Text.RegularExpressions;

namespace KnowitJulekalender2021.Dag14;

public class Dag14 : ISolution
{
    public void Execute()
    {
        var ordliste = File.ReadAllLines($"{AppContext.BaseDirectory}\\Dag14\\ordliste.txt");
        var count = 0;
        var nisseRegex = new Regex(@"^[^n].*n.{0,2}i.{0,2}s.{0,2}s.{0,2}e.{0,2}.*[^e]$");
        var trollRegex = new Regex(@"^.*t.{1,5}r.{1,5}o.{1,5}l.{1,5}l.*$");

        foreach (var ord in ordliste)
        {
            if (nisseRegex.IsMatch(ord) || trollRegex.IsMatch(ord))
            {
                count++;
            }            

        }

        Console.WriteLine(count);
    }
}
