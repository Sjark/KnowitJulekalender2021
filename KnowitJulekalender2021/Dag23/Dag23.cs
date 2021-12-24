namespace KnowitJulekalender2021.Dag23;

public class Dag23 : ISolution
{
    public void Execute()
    {
        var input = File.ReadAllLines($"{AppContext.BaseDirectory}\\Dag23\\maze.txt")
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

        var visited = new HashSet<Coordinate>();
        var queue = new Queue<(Coordinate, int Length)>();
        queue.Enqueue((new Coordinate(0, 0), 0));

        var steps = 0;

        while (queue.Count > 0)
        {
            var (cord, length) = queue.Dequeue();

            if (cord.X == input.Count - 1 && cord.Y == input.Count - 1)
            {
                steps = length;
                break;
            }

            var cell = maze[cord.X, cord.Y];

            visited.Add(cord);

            var east = new Coordinate(cord.X + 1, cord.Y);
            if (cell.East && !visited.Contains(east))
            {
                queue.Enqueue((east, length + 1));
            }

            var west = new Coordinate(cord.X - 1, cord.Y);
            if (cell.West && !visited.Contains(west))
            {
                queue.Enqueue((west, length + 1));
            }

            var north = new Coordinate(cord.X, cord.Y - 1);
            if (cell.North && north.Y >= 0 && !visited.Contains(north))
            {
                queue.Enqueue((north, length + 1));
            }

            var south = new Coordinate(cord.X, cord.Y + 1);
            if (cell.South && !visited.Contains(south))
            {
                queue.Enqueue((south, length + 1));
            }
        }

        Console.WriteLine(steps);
    }
}

public record Coordinate(int X, int Y);
public record MazeCell(bool North, bool East, bool South, bool West);