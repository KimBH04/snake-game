public struct Snake(int r, int c, State direction = State.Up)
{
    public (int r, int c) Head = (r, c);

    public (int r, int c) Tail = (r, c);

    public State Direction = direction;
}