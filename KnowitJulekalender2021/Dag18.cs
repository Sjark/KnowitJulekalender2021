namespace KnowitJulekalender2021;

public class Dag18 : ISolution
{
    public void Execute()
    {
        long sum = 0;
        var lockObject = new object();

        Parallel.For(1, 1000001, i =>
        {
            var niklatz = NiklatzSequence(i);
            var collatz = CollatzSequence(i);

            if (niklatz.Length != collatz.Length)
            {
                lock (lockObject)
                {
                    sum += niklatz.Sum;
                }
            }
        });

        Console.WriteLine(sum);
    }

    private static (long Sum, long Length) NiklatzSequence(long n)
    {
        var turnCount = 0;
        long sum = n;
        long length = 1;

        do
        {
            if (n % 37 == 0)
            {
                turnCount = 3;
            }

            if ((turnCount == 0 && n % 2 == 0) || (turnCount > 0 && n % 2 == 1))
            {
                n = n / 2;
            }
            else if ((turnCount == 0 && n % 2 == 1) || (turnCount > 0 && n % 2 == 0))
            {
                n = n * 3 + 1;
            }

            if (turnCount > 0)
            {
                turnCount--;
            }

            sum += n;
            length++;
        }
        while (n != 1);

        return (sum, length);
    }

    private static (long Sum, long Length) CollatzSequence(long n)
    {
        long sum = n;
        long length = 1;

        do
        {
            if (n % 2 == 0)
            {
                n = n / 2;
            }
            else
            {
                n = n * 3 + 1;
            }

            sum += n;
            length++;
        }
        while (n != 1);

        return (sum, length);
    }
}
