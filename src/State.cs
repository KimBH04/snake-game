[Flags]
public enum State
{
    None  = 0b00000,
    Up    = 0b00001,
    Left  = 0b00010,
    Down  = 0b00100,
    Right = 0b01000,
    Body  = 0b01111,
    Apple = 0b10000,
}