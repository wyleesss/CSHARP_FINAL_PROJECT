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

internal class User
{
    public int id { get; }
    public string user_name { get; set; }
    public string login { get; }
    public string password { get; set; }
    public string gmail { get; set; }

    public long durak_b { get; set; }
    public long black_jack_b { get; set; }

    public int golden_cards { get; set; }

    List<Achivements> achives = new();
    List<SpecialAbilities> durak_abilities = new();

    public User(int id, string user_name, string login, string password, string gmail,
                long durak_b, long black_jack_b, int golden_cards,
                List<Achivements> achives, List<SpecialAbilities> durak_abilities)
    {
        this.id = id;
        this.user_name = user_name;
        this.login = login;
        this.password = password;
        this.gmail = gmail;
        this.durak_b = durak_b;
        this.black_jack_b = black_jack_b;
        this.golden_cards = golden_cards;
        this.achives = achives;
        this.durak_abilities = durak_abilities;
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