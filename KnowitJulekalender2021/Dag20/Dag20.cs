namespace KnowitJulekalender2021.Dag20;

public class Dag20 : ISolution
{
    public void Execute()
    {
        var input = File.ReadAllLines($"{AppContext.BaseDirectory}\\Dag20\\maze.txt")
            .Select(a => a[1..^1].Split(")("))
            .ToList();

        var maze = new MazeCell[input.Count, input.Count];

        for (int x = 0; x < input.Count; x++)
        {
            for (int y = 0; y < input.Count; y++)
            {
                var cell = input[y][x].Split(',');

                maze[x, y] = new MazeCell(cell[0] == "1", cell[1] == "1", cell[2] == "1", cell[3] == "1");
            }
        }

        var direction = 'S';
        var currentXPos = 0;
        var currentYPos = 0;
        var steps = 0;

        while (currentXPos != (input.Count - 1) || currentYPos != (input.Count - 1))
        {
            var currentCell = maze[currentXPos, currentYPos];

            switch (direction)
            {
                case 'N':
                    if (currentCell.West)
                    {
                        direction = 'W';
                        currentXPos--;
                    }
                    else if (currentCell.North)
                    {
                        currentYPos--;
                    }
                    else if (currentCell.East)
                    {
                        currentXPos++;
                        direction = 'E';
                    }
                    else
                    {
                        currentYPos++;
                        direction = 'S';
                    }
                    break;
                case 'E':
                    if (currentCell.North)
                    {
                        direction = 'N';
                        currentYPos--;
                    }
                    else if (currentCell.East)
                    {
                        currentXPos++;
                    }
                    else if (currentCell.South)
                    {
                        currentYPos++;
                        direction = 'S';
                    }
                    else
                    {
                        currentXPos--;
                        direction = 'W';
                    }
                    break;
                case 'S':
                    if (currentCell.East)
                    {
                        direction = 'E';
                        currentXPos++;
                    }
                    else if (currentCell.South)
                    {
                        currentYPos++;
                    }
                    else if (currentCell.West)
                    {
                        currentXPos--;
                        direction = 'W';
                    }
                    else
                    {
                        currentYPos--;
                        direction = 'N';
                    }
                    break;
                case 'W':
                    if (currentCell.South)
                    {
                        direction = 'S';
                        currentYPos++;
                    }
                    else if (currentCell.West)
                    {
                        currentXPos--;
                    }
                    else if (currentCell.North)
                    {
                        currentYPos--;
                        direction = 'N';
                    }
                    else
                    {
                        currentXPos++;
                        direction = 'E';
                    }
                    break;
            }

            steps++;
        }

        Console.WriteLine(steps);
    }
}

public record MazeCell(bool North, bool East, bool South, bool West);
