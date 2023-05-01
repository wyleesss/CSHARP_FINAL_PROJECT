﻿namespace BJ
{
    class Black_Jack
    {
        bool leaver_d = false, leaver_p = false;
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
            d.score = 0;
            p.score = 0;

            foreach (Card card in d.hand)
            {
                if (card.number > 9)
                {
                    if(card.number !=14)
                    {
                        d.score += 10;
                    }
                }
                else
                {
                    d.score += card.number;
                }
            }
            foreach (Card card in d.hand)
            {
                if (card.number == 14)
                {
                    d.score2 = d.score;
                    d.score += 1;
                    d.score2 += 11;
                    leaver_d=true;
                }
            }
            Console.Write($"total: {d.score}");
            if (d.score2 > 0 && d.score2 < 22) {
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
                    if (card.number != 14)
                    {
                        p.score += 10;
                    }
                }
                else
                {
                    p.score += card.number;
                }
            }
            foreach (Card card in p.hand)
            {
                if (card.number == 14)
                {
                    p.score2 = p.score;
                    p.score += 1;
                    p.score2 += 11;
                    leaver_p = true;
                }
            }
            Console.Write($"total: {p.score}");
            if (p.score2 > 0 && p.score2 < 22)
            {
                Console.Write($"/{p.score2}");
            }
            Console.WriteLine();
        }
        public void BJ_init()
        {
            int bid = p.bid();
            p.hand.Add(deck[0]);
            deck.Remove(deck[0]);
            p.hand.Add(deck[0]);
            deck.Remove(deck[0]);
            d.hand.Add(deck[0]);
            deck.Remove(deck[0]);
            print_menu();
            while (true)
            {
                Console.Clear();
                print_menu();
                Console.WriteLine("Write your choice:");
                p.menu();
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        p.hand.Add(deck[0]);
                        deck.Remove(deck[0]);
                        Console.Clear();
                        print_menu();
                        if (p.score > 21)
                        {
                            Console.WriteLine("YOU LOSE");
                            return;
                        }
                        break;
                    case 2:
                        Dealer_draw();
                        Check();
                        return;
                    case 3:
                        //user.black_jack_b-=bid;
                        //bid*=2;
                        p.hand.Add(deck[0]);
                        deck.Remove(deck[0]);
                        Console.Clear();
                        print_menu();
                        if (p.score > 21)
                        {
                            Console.WriteLine("YOU LOSE");
                            return;
                        }
                        break;
                }
            }

        }
        public void Dealer_draw()
        {
            while (d.score < p.score && d.score < 17)
            {
                d.hand.Add(deck[0]);
                deck.Remove(deck[0]);
                Console.Clear();
                print_menu();
            }
        }
        public void Check()
        {
            int biggest_p = p.score;
            int biggest_d = d.score;
            if (p.score2 != 0  && p.score2 < 22)
            {
                biggest_p = p.score2;
            }
            if (d.score2 != 0 && d.score2 < 22)
            {
                biggest_d = d.score2;
            }
            if (biggest_p > 21)
            {
                Console.WriteLine("YOU LOSE");
            }
            else if (biggest_d > 21)
            {
                Console.WriteLine("YOU WIN");
                //user.black_jack_b+= bid*2;
            }
            else if (biggest_d > biggest_p)
            {
                Console.WriteLine("YOU LOSE");
            }
            else if (biggest_p > biggest_d)
            {
                Console.WriteLine("YOU WIN");
                //user.black_jack_b+= bid*2;
            }else if(biggest_p == biggest_d)
            {
                Console.WriteLine("TIE");
                //user.black_jack_b+= bid;
            }
        }
    }

    class Player
    {
        public List<Card> hand = new List<Card>();
        public int score = 0;
        public int score2 = 0;
        public void menu()
        {
            Console.WriteLine("1 - draw");
            Console.WriteLine("2 - stop");
            Console.WriteLine("3 - double");
        }
        public int bid()
        {
            //Console.WriteLine("Write your bet");
            //bid = Console.ReadLine();
            //user.black_jack_b-=bid;
            return 0; 
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