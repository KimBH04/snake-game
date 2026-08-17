Console.Clear();
Console.WriteLine("Snake Game");
var input = (string s) =>
{
    for (;;)
    {
        Console.Write(s);
        if (int.TryParse(Console.ReadLine(), out var i))
        {
            return i;
        }
        Console.WriteLine("잘못된 숫자.");
    }
};

int depth = input("depth: ");
int width = input("width: ");

Game game = new(depth, width);
Printer<Game> printer = new(Translate);
printer.Print(game);

for (;;)
{
    var key = Input.GetLastKeyInBuffer();
    game.SetSnakeDirection(key switch
    {
        ConsoleKey.UpArrow    => State.Up,
        ConsoleKey.LeftArrow  => State.Left,
        ConsoleKey.DownArrow  => State.Down,
        ConsoleKey.RightArrow => State.Right,
        _ => State.None,
    });

    if (!game.Next())
    {
        break;
    }

    Console.Clear();
    printer.Print(game);

    await Task.Delay(333);
}

static string Translate(Game g)
{
    System.Text.StringBuilder sb = new();
    for (int i = 0; i < g.Width + 2; i++)
    {
        sb.Append("[]");
    }
    sb.AppendLine();

    var board = g.Board;
    for (int r = 0; r < g.Depth; r++)
    {
        sb.Append("[]");
        for (int c = 0; c < g.Width; c++)
        {
            if      (board[r, c] == State.None)  sb.Append("  ");
            else if (board[r, c] == State.Apple) sb.Append("+0");
            else   /*board[r, c] |  State.Body*/ sb.Append("<>");
        }
        sb.AppendLine("[]");
    }

    for (int i = 0; i < g.Width + 2; i++)
    {
        sb.Append("[]");
    }
    sb.AppendLine();

    sb.AppendLine($"score: {g.Score}");

    return sb.ToString();
}