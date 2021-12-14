namespace KnowitJulekalender2021.Dag13;

public class Dag13 : ISolution
{
    public void Execute()
    {
        var moves = File.ReadAllLines($"{AppContext.BaseDirectory}\\Dag13\\moves.txt");
        var grid = new bool[9, 9, 250];

        foreach (var move in moves)
        {
            var currentX = 4;
            var currentY = 4;
            var currentZ = 249;

            foreach (var step in move)
            {
                if (grid[currentX, currentY, currentZ - 1] || currentZ == 0)
                {
                    break;
                }

                currentZ--;

                switch (step)
                {
                    case 'N':
                        if (currentX != 0 && !grid[currentX - 1, currentY, currentZ])
                        {
                            currentX--;
                        }
                        break;
                    case 'S':
                        if (currentX != 8 && !grid[currentX + 1, currentY, currentZ])
                        {
                            currentX++;
                        }
                        break;
                    case 'W':
                        if (currentY != 0 && !grid[currentX, currentY - 1, currentZ])
                        {
                            currentY--;
                        }
                        break;
                    case 'E':
                        if (currentY != 8 && !grid[currentX, currentY + 1, currentZ])
                        {
                            currentY++;
                        }
                        break;
                }
            }

            while (currentZ != 0 && !grid[currentX, currentY, currentZ - 1])
            {
                currentZ--;
            }

            grid[currentX, currentY, currentZ] = true;
        }

        var height = 0;

        for (var z = 0; z < 250; z++)
        {
            var found = false;

            for (var y = 0; y < 9; y++)
            {
                
                for (var x = 0; x < 9; x++)
                {
                    if (grid[x, y, z])
                    {
                        found = true;
                        break;
                    }
                }

                if (found)
                {
                    break;
                }
            }

            if (found)
            {
                height += 10;
            }
            else
            {
                break;
            }
        }

        Console.WriteLine(height);
    }
}
