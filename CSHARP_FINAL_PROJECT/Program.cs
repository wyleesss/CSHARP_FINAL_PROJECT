using Durak;
using System.Text;
using static UserInterface;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        User user = sign_menu();
        User test = new("test", "test", "test", 0, 0, 0, new(), new());
        main_menu(user);

        /*                Game game = new(11, new("test", new()), new());
                        game.Start();*/

        //        BJ.Black_Jack bj = new BJ.Black_Jack();
        //        bj.BJ_init();
        //

        //BinaryFormatter binaryFormatter = new();

        //User user = new("zxc", "zxc", "666", 666, 666, 666, new(), new());

        //using (Stream fStream = File.Create("DATA.bin"))
        //{
        //    #pragma warning disable SYSLIB0011
        //    binaryFormatter.Serialize(fStream, user);
        //    fStream.Close();
        //}

        //using (Stream fStream = File.OpenRead("DATA.bin"))
        //{
        //    #pragma warning disable SYSLIB0011
        //    //binaryFormatter.Deserialize(fStream);??????????
        //    fStream.Close();
        //}
    }
}