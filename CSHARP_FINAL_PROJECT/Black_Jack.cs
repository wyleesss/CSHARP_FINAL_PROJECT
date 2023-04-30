namespace BJ
{
    class Black_Jack
    {
        public static List<Card> deck = new List<Card>();
        public Player p { get; set; } = new Player();
        Dealer d = new Dealer();
        public Black_Jack()
        {
            deck = Card.Schuffle(2);
            deck = Card.Mix(deck);
        }

        public void print_menu()
        {
            foreach (Card card in d.hand)
            {
                if (card.number > 9)
                {
                    if (d.score == 14)
                    {
                        d.score2 = d.score;
                        d.score += 1;
                        d.score2 += 11;
                    }
                    else
                    {
                        d.score += 10;
                    }
                }
                d.score += card.number;

            }
            Console.Write($"total: {d.score}");
            if (d.score2 > 0) {
                Console.Write($"/{d.score2}");
            }
            Console.WriteLine();
            Console.Write($"Dealer:                      ");
            foreach(Card card in d.hand)
            {
                card.print();

            }
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.Write("You:                         ");
            foreach (Card card in p.hand)
            {
                card.print();
            }
            Console.WriteLine();
            foreach (Card card in p.hand)
            {
                if (card.number > 9)
                {
                    if (p.score == 14)
                    {
                        p.score2 = p.score;
                        p.score += 1;
                        p.score2 += 11;
                    }
                    else
                    {
                        p.score += 10;
                    }
                }
                else
                {
                    p.score += card.number;
                }
            }
            Console.Write($"total: {p.score}");
            if (p.score2 > 0)
            {
                Console.Write($"/{p.score2}");
            }
            Console.WriteLine();
        }
        public void BJ_init()
        {
            p.hand.Add(deck[0]);
            deck.Remove(deck[0]);
            p.hand.Add(deck[0]);
            deck.Remove(deck[0]);
            d.hand.Add(deck[0]);
            deck.Remove(deck[0]);

            print_menu();


        }
    }

    class Player
    {
        public List<Card> hand = new List<Card>();
        public int score = 0;
        public int score2 = 0;
        public void bid()
        {
            
        }
    }

    class Dealer
    {
        public Card remembered = new Card();
        public List<Card> hand = new List<Card>();
        public int score = 0;
        public int score2 = 0;
    }
}