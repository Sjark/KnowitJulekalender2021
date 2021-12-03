using System.Globalization;

namespace KnowitJulekalender2021.Dag3;

public class Dag3 : ISolution
{
    public void Execute()
    {
        var neightberhood = File.ReadAllText($"{AppContext.BaseDirectory}\\Dag3\\input.txt").AsSpan();

        var length = 0;
        var index = 0;

        for (int i = 0; i < neightberhood.Length; i++)
        {
            var jCount = 0;
            var nCount = 0;

            for (int y = i; y < neightberhood.Length; y++)
            {
                if (neightberhood[y] == 'J')
                {
                    jCount++;
                }
                else
                {
                    nCount++;
                }

                if (jCount == nCount && y - i + 1 > length)
                {
                    length = y - i + 1;
                    index = i;
                }
            }
        }

        Console.WriteLine($"{length}, {index}");
    }
}