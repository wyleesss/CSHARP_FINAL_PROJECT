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

class Card
{
    public char suit  { get; }
    public int number { get; }

    public Card(int number, char suit)
    {
        this.number = number;
        this.suit = suit;
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
        List<Card> temp = deck;
        deck.Clear();

        Random random = new();
        int random_index;

        while (temp.Count > 0)
        {
            random_index = random.Next(0, temp.Count - 1);

            deck.Add(temp[random_index]);
            temp.RemoveAt(random_index);
        }

        return deck;
    }
}