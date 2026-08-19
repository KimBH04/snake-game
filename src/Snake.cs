public struct Snake
{
    public (int r, int c) Head;

    public (int r, int c) Tail;

    public State Direction;

    public Snake(int r, int c, State direction = State.Up)
    {
        Head = (r, c);
        Tail = (r, c);
        Direction = direction;
    }
}