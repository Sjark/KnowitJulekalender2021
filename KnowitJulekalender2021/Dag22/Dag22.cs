using System.Text;

namespace KnowitJulekalender2021.Dag22;

public class Dag22 : ISolution
{
    public void Execute()
    {
        var boards = File.ReadAllLines($"{AppContext.BaseDirectory}\\Dag22\\boards.txt", Encoding.UTF8);

        var testBoard = "🎅🎅⛄🎄✨⛄⛄🎅🎄✨✨⛄🎅🎄🎄✨";

        var solutionBoard = "🎅🎅🎅🎅⛄⛄⛄⛄✨✨✨✨🎄🎄🎄🎄";

        var solutionBoardParsed = new Board(solutionBoard);

        var possibleSolutions = new Dictionary<string, int>();

        var queue = new Queue<(Board Board, int Depth)>();
        queue.Enqueue((solutionBoardParsed, 0));

        while (queue.Count > 0)
        {
            var (currentBoard, depth) = queue.Dequeue();

            var currentBoardString = currentBoard.ToString();
            
            if (possibleSolutions.ContainsKey(currentBoardString))
            {
                continue;
            }

            possibleSolutions.Add(currentBoardString, depth);

            for (int i = 0; i < 4; i++)
            {
                for (int y = 0; y < 4; y++)
                {
                    var b = new Board((int[,])currentBoard._board.Clone());

                    if (y == 0)
                    {
                        b.RotateRow(i, false);
                    }
                    else if (y == 1)
                    {
                        b.RotateRow(i, true);
                    }
                    else if (y == 2)
                    {
                        b.RotateColumn(i, false);
                    }
                    else
                    {
                        b.RotateColumn(i, true);
                    }

                    if (!possibleSolutions.ContainsKey(b.ToString()) && depth <= 5)
                    {
                        queue.Enqueue((b, depth + 1));
                    }
                }
            }
        }

        var sum = 0;

        foreach (var board in boards)
        {
            sum += Board.Solve(new Board(board), possibleSolutions);
        }

        Console.WriteLine(sum);
    }
}

public class Board
{
    public readonly int[,] _board = new int[4, 4];
    private readonly List<int> _solution = new List<int> { 127877, 9924, 10024, 127876 };

    public Board(string board)
    {
        Console.OutputEncoding = Encoding.UTF8;

        var count = 0;

        foreach (var rune in board.EnumerateRunes())
        {
            _board[count / 4, count % 4] = rune.Value;
            count++;
        }
    }

    public Board(int[,] board)
    {
        _board = board ?? throw new ArgumentNullException(nameof(board));
    }

    public void RotateRow(int rowNumber, bool left)
    {
        if (left)
        {
            var temp = _board[rowNumber, 0];
            _board[rowNumber, 0] = _board[rowNumber, 1];
            _board[rowNumber, 1] = _board[rowNumber, 2];
            _board[rowNumber, 2] = _board[rowNumber, 3];
            _board[rowNumber, 3] = temp;
        }
        else
        {
            var temp = _board[rowNumber, 0];
            _board[rowNumber, 0] = _board[rowNumber, 3];
            _board[rowNumber, 3] = _board[rowNumber, 2];
            _board[rowNumber, 2] = _board[rowNumber, 1];
            _board[rowNumber, 1] = temp;
        }
    }

    public void RotateColumn(int columnNumber, bool up)
    {
        if (up)
        {
            var temp = _board[0, columnNumber];
            _board[0, columnNumber] = _board[1, columnNumber];
            _board[1, columnNumber] = _board[2, columnNumber];
            _board[2, columnNumber] = _board[3, columnNumber];
            _board[3, columnNumber] = temp;
        }
        else
        {
            var temp = _board[0, columnNumber];
            _board[0, columnNumber] = _board[3, columnNumber];
            _board[3, columnNumber] = _board[2, columnNumber];
            _board[2, columnNumber] = _board[1, columnNumber];
            _board[1, columnNumber] = temp;
        }
    }

    public override string ToString()
    {
        var sb = new StringBuilder();

        for (int i = 0; i < 4; i++)
        {
            for (int y = 0; y < 4; y++)
            {
                var rune = new Rune(_board[i, y]).ToString();
                sb.Append(rune);
            }
        }
        return sb.ToString();
    }

    public static int Solve(Board board, Dictionary<string, int> possibleSolutions)
    {
        var queue = new Queue<(Board Board, int Depth)>();
        queue.Enqueue((board, 0));

        var visited = new HashSet<string>();
        var steps = 0;

        while (queue.Count > 0)
        {
            var (currentBoard, depth) = queue.Dequeue();

            var currentBoardString = currentBoard.ToString();

            if (possibleSolutions.ContainsKey(currentBoardString))
            {
                steps = depth + possibleSolutions[currentBoardString];
                break;
            }

            visited.Add(currentBoardString);

            for (int i = 0; i < 4; i++)
            {
                for (int y = 0; y < 4; y++)
                {
                    var b = new Board((int[,])currentBoard._board.Clone());
                    
                    if (y == 0)
                    {
                        b.RotateRow(i, false);
                    }
                    else if (y == 1)
                    {
                        b.RotateRow(i, true);
                    }
                    else if (y == 2)
                    {
                        b.RotateColumn(i, false);
                    }
                    else
                    {
                        b.RotateColumn(i, true);
                    }
                    
                    if (!visited.Contains(b.ToString()) && depth < 16)
                    {
                        queue.Enqueue((b, depth + 1));
                    }
                }
            }
        }

        return steps;
    }

    public bool IsSolved()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int y = 0; y < 4; y++)
            {
                if (_solution[i] != _board[i, y])
                {
                    return false;
                }
            }
        }

        return true;
    }
}
