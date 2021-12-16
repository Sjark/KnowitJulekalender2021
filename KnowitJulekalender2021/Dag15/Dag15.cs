namespace KnowitJulekalender2021.Dag15;

public class Dag15 : ISolution
{
    public void Execute()
    {
        var input = @"wawwgjlmwkafeosjoæiralop
jagwfjsuokosjpzæynzxtxfnbæjkæalektfamxæø
wawwgjlmwkoåeosaæeoltååøbupscpfzqehkgdhkjdoqqkuuakvwogjkpøjsbmpq
vttyøyønøbjåiåzpejsimøldajjecnbplåkyrsliænhbgkvbecvdscxømrvåmagdioftvivwøkvbnyøå";

        var dict = new Dictionary<char, int>
        {
            { 'a', 1 },
            { 'b', 2 },
            { 'c', 3 },
            { 'd', 4 },
            { 'e', 5 },
            { 'f', 6 },
            { 'g', 7 },
            { 'h', 8 },
            { 'i', 9 },
            { 'j', 10 },
            { 'k', 11 },
            { 'l', 12 },
            { 'm', 13 },
            { 'n', 14 },
            { 'o', 15 },
            { 'p', 16 },
            { 'q', 17 },
            { 'r', 18 },
            { 's', 19 },
            { 't', 20 },
            { 'u', 21 },
            { 'v', 22 },
            { 'w', 23 },
            { 'x', 24 },
            { 'y', 25 },
            { 'z', 26 },
            { 'æ', 27 },
            { 'ø', 28 },
            { 'å', 29 }
        };

        foreach (var line in input.Split(Environment.NewLine))
        {
            var m = 1;
            var pos = 0;
            var key = "alvalvxx".ToCharArray();
            var keyLength = 6;
            var spanLine = line.AsSpan();

            while (pos < spanLine.Length)
            {
                var currentBlock = spanLine.Slice(pos, 8);
                var decrypted = new char[8];

                for (var y = 0; y < 8; y++)
                {
                    var toDecrypt = dict[currentBlock[y]];

                    toDecrypt = ((toDecrypt - (keyLength * m) + 29) % 29);
                    toDecrypt = ((toDecrypt - (y + 1) + 29) % 29);
                    toDecrypt = ((toDecrypt - dict[key[y]] + 29) % 29);

                    if (toDecrypt == 0)
                    {
                        toDecrypt = 29;
                    }

                    decrypted[y] = dict.Where(a => a.Value == toDecrypt).Select(a => a.Key).First();
                }

                Console.Write(new string(decrypted));

                m++;
                pos += 8;
            }

            Console.WriteLine();
        }
    }
}
