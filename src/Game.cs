public class Game
{
    public const int MIN = 3;
    public const int MAX = 1000;

    public readonly int Depth;

    public readonly int Width;

    private readonly State[,] board;

    private readonly Dictionary<State, (int r, int c)> Direction = new()
    {
        { State.Up    , (-1,  0)},
        { State.Left  , ( 0, -1)},
        { State.Down  , ( 1,  0)},
        { State.Right , ( 0,  1)},
    };

    private Snake Snake;

    public int Score { get; private set; }

    public State[,] Board => (State[,])board.Clone();

    public Game(int depth, int width)
    {
        // 범위 밖
        // depth 또는 width가 MIN보다 작으면 오버플로우
        if ((uint)depth - MIN >= MAX - MIN || (uint)width - MIN >= MAX - MIN)
        {
            depth = 20;
            width = 20;
        }

        Depth = depth;
        Width = width;

        Snake = new(depth / 2, width / 2);
        board = new State[depth, width];
        board[depth / 2, width / 2] = Snake.Direction;
        Score = 0;

        PlaceApple();
    }

    public void SetSnakeDirection(State dir)
    {
        if ((dir & State.Body) == State.None)
        {
            return;
        }

        Snake.Direction = dir;
    }

    public bool Next()
    {
        var nextR = Snake.Head.r + Direction[Snake.Direction].r;
        var nextC = Snake.Head.c + Direction[Snake.Direction].c;
        if (Movable(nextR, nextC))
        {
            board[Snake.Head.r, Snake.Head.c] = Snake.Direction;

            if (board[nextR, nextC] == State.Apple)
            {
                Score++;
                if (!PlaceApple())
                {
                    return false;
                }
            }
            else    // 꼬리 옮기기
            {
                var dir = board[Snake.Tail.r, Snake.Tail.c];
                board[Snake.Tail.r, Snake.Tail.c] = State.None;
                
                Snake.Tail.r += Direction[dir].r;
                Snake.Tail.c += Direction[dir].c;
            }

            Snake.Head = (nextR, nextC);
            board[nextR, nextC] = Snake.Direction;

            return true;
        }

        return false;
    }

    private bool Movable(int r, int c) =>
        (uint)r < Depth && (uint)c < Width && (board[r, c] & State.Body) == State.None;

    private bool PlaceApple()
    {
        int den = Depth * Width - (Score + 1);
        for (int r = 0; r < Depth; r++)
        {
            for (int c = 0; c < Width; c++)
            {
                if (board[r, c] != State.None)
                {
                    continue;
                }

                if (den <= 0)
                {
                    return false;
                }

                if (Random.Shared.NextInt64() % den == 0)
                {
                    board[r, c] = State.Apple;
                    return true;
                }
                else
                {
                    den--;
                }
            }
        }

        return false;
    }
}