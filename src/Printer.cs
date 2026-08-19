public class Printer<T>
{
    private readonly Func<T, string> translator;

    public Printer(Func<T, string> translator)
    {
        this.translator = translator ?? (o => o?.ToString() ?? "null");
    }

    public void Print(T o)
    {
        var messsage = translator.Invoke(o);
        Console.Write(messsage);
    }
}