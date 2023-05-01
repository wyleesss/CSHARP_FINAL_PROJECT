using System.Text;
class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        Durak.Game game = new(9, new("also_bot", new()), new());

        game.Start();
    }
}