namespace KnowitJulekalender2021.Dag5;

public class Dag5 : ISolution
{
    public void Execute()
    {
        var tree = File.ReadAllText($"{AppContext.BaseDirectory}\\Dag5\\tree.txt").AsSpan();
        //var tree = "Aurora(Toralv(Grinch(Kari Robinalv) Alvborg) Grinch(Alva(Alve-Berit Anna) Grete(Ola Hans)))".AsSpan();

        var currentDepth = 0;
        var grinches = new List<int>();
        var maxDepth = 0;
        var currentPointer = 0;

        while (currentPointer < tree.Length)
        {
            var currentCharacter = tree[currentPointer];

            if (currentCharacter == '(')
            {
                currentDepth++;

                if (maxDepth < (currentDepth - grinches.Count))
                {
                    maxDepth = (currentDepth - grinches.Count);
                }
            }
            else if (currentCharacter == ')')
            {
                currentDepth--;

                var grinchesAtHeigherDepth = grinches.Where(a => a > currentDepth).ToList();

                foreach (var grinch in grinchesAtHeigherDepth)
                {
                    grinches.Remove(grinch);
                }
            }
            else if (currentCharacter == 'G' && tree.Slice(currentPointer).StartsWith("Grinch"))
            {
                grinches.Add(currentDepth);
                currentPointer += 5;
            }
            
            currentPointer++;
        }

        Console.WriteLine(maxDepth);
    }
}
