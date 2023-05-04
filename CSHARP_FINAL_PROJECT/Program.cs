using System.Text;
using static UserInterface;

class Program
{
    static void Main()
    {
        //2700
        //final
        Console.OutputEncoding = Encoding.UTF8;
        User user = sign_menu();
        main_menu(user);
    }
}