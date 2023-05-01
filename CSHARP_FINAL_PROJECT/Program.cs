using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using static MainMenu;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        Sign_in_Menu();


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