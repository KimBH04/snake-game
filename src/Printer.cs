public class Printer<T>(Func<T, string> translator)
{
    private readonly Func<T, string> translator = translator ?? (o => o?.ToString() ?? "null");

    public void Print(T o)
    {
        var messsage = translator.Invoke(o);
        Console.Write(messsage);
    }
}