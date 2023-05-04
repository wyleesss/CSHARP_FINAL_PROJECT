using Durak;
using System.Text;
using static UserInterface;

class Program
{
    static void Main()
    {
        //2700
        Console.OutputEncoding = Encoding.UTF8;
        User user = sign_menu();
        User test = new("test", "test", "test", 0, 0, 0, new(), new());
        main_menu(user);

    }
}