using System.Text;
class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        BJ.Black_Jack bj = new BJ.Black_Jack();
        bj.BJ_init();
    }
}