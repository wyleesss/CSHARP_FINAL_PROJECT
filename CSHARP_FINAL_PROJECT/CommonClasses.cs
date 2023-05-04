using System.Runtime.Serialization.Formatters.Binary;
#pragma warning disable SYSLIB0011
static class SerialDB
{
    static BinaryFormatter formatter = new();

    internal static bool check(string login, string password)
    {
        List<User> users = new List<User>();
        try
        {
            using (Stream fStream = File.OpenRead("..\\..\\..\\DB.bin"))
            {
                users = (List<User>)formatter.Deserialize(fStream);
            }
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex);
        }
        foreach (User i in users)
        {
            if (i.login == login && i.password == password)
            {
                return true;
            }
        }
        Console.WriteLine("Wrong data");
        return false;
    }




    internal static bool reg_check(string login)
    {
        List<User> users = new List<User>();
        try
        {
            using (Stream fStream = File.OpenRead("..\\..\\..\\DB.bin"))
            {
                users = (List<User>)formatter.Deserialize(fStream);
            }
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex);
        }
        foreach (User i in users)
        {
            if (i.login == login)
            {
                UserInterface.set_and_print("User is already exist");
                return false;
            }
        }
        return true;
    }







    internal static void push(User user)
    {
        List<User> users = new List<User>();
        try
        {
            using (Stream fStream = File.OpenRead("..\\..\\..\\DB.bin"))
            {
                users = (List<User>)formatter.Deserialize(fStream);
            }

            users.Add(new User(user.user_name, user.login, user.password, user.durak_b, user.black_jack_b, user.golden_cards, new(), new()));

            using (var st = new FileStream("..\\..\\..\\DB.bin", FileMode.Create, FileAccess.Write) )
            {
                formatter.Serialize(st, users);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    internal static User take(string login, string password)
    {
        List <User> users = new List<User>();
        try
        {
            using (Stream fStream = File.OpenRead("..\\..\\..\\DB.bin"))
            {
                users = (List<User>)formatter.Deserialize(fStream);
            }
        }
        catch (Exception)
        {

            throw;
        }
        foreach (User i in users)
        {
            if (i.login == login && i.password == password)
            {
                return new User(i.user_name, i.login, i.password, i.durak_b, i.black_jack_b, i.golden_cards, new(), new());
            }
        }
        throw new Exception("Wrong data");
    }
}

[Serializable]
class Achivements
{
    public string name { get; }
    public long   award_money { get; }
    public int    golden_cards { get; }
    public string type { get; }
    public string game { get; }

    public Achivements(string name, long award_money, int golden_cards,
                       string type, string game)
    {
        this.name = name;
        this.award_money = award_money;
        this.golden_cards = golden_cards;
        this.type = type;
        this.game = game;
    }
}

[Serializable]
class SpecialAbilities
{
    string name { get; }
    string type { get; }

    public SpecialAbilities(string name, string type)
    {
        this.name = name;
        this.type = type;
    }
    public void to_do() {  }
}

[Serializable]
internal class User : ICloneable
{
    public string user_name { get; set; }
    public string login { get; }
    public string password { get; set; }

    public long durak_b { get; set; }
    public long black_jack_b { get; set; }

    public int golden_cards { get; set; }

    List<Achivements> achives;
    List<SpecialAbilities> durak_abilities;

    public User(string user_name, string login, string password,
                long durak_b, long black_jack_b, int golden_cards,
                List<Achivements> achives, List<SpecialAbilities> durak_abilities)
    {
        this.user_name = user_name;
        this.login = login;
        this.password = password;
        this.durak_b = durak_b;
        this.black_jack_b = black_jack_b;
        this.golden_cards = golden_cards;
        this.achives = achives;
        this.durak_abilities = durak_abilities;
    }

    public object Clone()
    {
        return MemberwiseClone();
    }
}

class Card : ICloneable
{
    public char suit { get; }
    public int number { get; }

    public Card()
    {

    }

    public Card(int number, char suit)
    {
        this.number = number;
        this.suit = suit;
    }
    public void print()
    {
        if (number < 11) 
            Console.Write(number);
        
        else
        {
            switch (number)
            {
                case 11:
                    Console.Write("J");
                    break;
                case 12:
                    Console.Write("Q");
                    break;
                case 13:
                    Console.Write("K");
                    break;
                case 14:
                    Console.Write("A");
                    break;
            }
        }

        Console.Write(suit);
    }

    static public List<Card> SortNum(List<Card> kol) 
    {
        Card buff = new();

        for (int i = 0; i < kol.Count(); i++) 
        {
            for (int j = 0; j < kol.Count() - i - 1; j++)
            {
                if (kol[j].number > kol[j + 1].number) 
                {
                    buff = kol[j];
                    kol[j] = kol[j + 1];
                    kol[j + 1] = buff;
                }
            }
        }

        return kol;
    }

    static public List<Card> SortSuit(List<Card> kol)
    {
        Card buff = new();

        for (int i = 0; i < kol.Count(); i++)
        {
            for (int j = 0; j < kol.Count() - i - 1; j++)
            {
                if (kol[j].suit > kol[j + 1].suit)
                {
                    buff = kol[j];
                    kol[j] = kol[j + 1];
                    kol[j + 1] = buff;
                }
            }
        }

        return kol;
    }

    static public List<Card> SortSuit(List<Card> kol, char trump)
    {
        Card buff = new();

        for (int i = 0; i < kol.Count(); i++)
        {
            for (int j = 0; j < kol.Count() - i - 1; j++)
            {
                if (kol[j].suit == trump && kol[j + 1].suit != trump)
                {
                    buff = kol[j];
                    kol[j] = kol[j + 1];
                    kol[j + 1] = buff;
                }
            }
        }

        return kol;
    }

    static public List<Card> Schuffle(int min_card, int decks_n = 1)
    {
        List<Card> deck = new();

        for (int i = 0; i < decks_n; i++)
        {
            for (int j = min_card; j < 15; j++)
            {
                for (int k = 1; k < 5; k++)
                {
                    switch (k)
                    {
                        case 1:
                            deck.Add(new Card(j, '♥'));
                            break;
                        case 2:
                            deck.Add(new Card(j, '♠'));
                            break;
                        case 3:
                            deck.Add(new Card(j, '♦'));
                            break;
                        case 4:
                            deck.Add(new Card(j, '♣'));
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        return deck;
    }

    static public List<Card> Mix(List<Card> deck)
    {
        List<Card> temp = new List<Card>();
        Random random = new();
        int random_index;

        while (deck.Count > 0)
        {
            random_index = random.Next(0, deck.Count - 1);

            temp.Add(deck[random_index]);
            deck.RemoveAt(random_index);
        }

        return temp;
    }

    public object Clone()
    {
        return MemberwiseClone();
    }

    public override string ToString()
    {
        string res = string.Empty;

        if (number < 11)
            res = number.ToString();

        else
        {
            switch (number)
            {
                case 11:
                    res = "J";
                    break;
                case 12:
                    res = "Q";
                    break;
                case 13:
                    res = "K";
                    break;
                case 14:
                    res = "A";
                    break;
            }
        }

        res += suit;

        return res;
    }
}