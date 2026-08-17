public static class Input
{
    public static ConsoleKey GetLastKeyInBuffer()
    {
        ConsoleKey key = ConsoleKey.None;

        while (Console.KeyAvailable)
        {
            key = Console.ReadKey().Key;
        }

        return key;
    }
}