namespace KnowitJulekalender2021.Dag6;

public class Dag6 : ISolution
{
    public void Execute()
    {
        var presents = File.ReadAllLines($"{AppContext.BaseDirectory}\\Dag6\\pakker.txt");
        var presentsGrid = new bool[21, presents.Length];
        var fallenPresents = 0;

        foreach (var present in presents)
        {
            var presentSize = present.Split(',').Select(a => int.Parse(a)).ToList();

            var yPos = 0;
            var foundY = false;

            while (!foundY)
            {
                var isEmpty = true;

                for (int i = presentSize[0]; i < (presentSize[0] + presentSize[1]); i++)
                {
                    if (presentsGrid[i, yPos])
                    {
                        isEmpty = false;
                        break;
                    }
                }

                if (isEmpty)
                {
                    foundY = true;
                }
                else
                {
                    yPos++;
                }
            }

            if (yPos == 0)
            {
                for (int i = presentSize[0]; i < (presentSize[0] + presentSize[1]); i++)
                {
                    presentsGrid[i, yPos] = true;
                }
            }
            else
            {
                var half = presentSize[1] / 2;
                var extra = presentSize[1] % 2 == 1 ? 1 : 0;

                var leftHalf = false;
                var rightHalf = false;

                for (int i = presentSize[0]; i < (presentSize[0] + presentSize[1]); i++)
                {
                    if (i < (presentSize[0] + half + extra) && presentsGrid[i, yPos - 1])
                    {
                        leftHalf = true;
                    }

                    
                    if (i >= (presentSize[0] + presentSize[1] - half - extra) && presentsGrid[i, yPos - 1])
                    {
                        rightHalf = true;
                    }
                }

                if (leftHalf && rightHalf)
                {
                    for (int i = presentSize[0]; i < (presentSize[0] + presentSize[1]); i++)
                    {
                        presentsGrid[i, yPos] = true;
                    }
                }
                else
                {
                    fallenPresents++;
                }
            }
        }

        Console.WriteLine(fallenPresents);
    }
}
