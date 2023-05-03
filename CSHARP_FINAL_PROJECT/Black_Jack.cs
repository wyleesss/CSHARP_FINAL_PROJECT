namespace BJ
{
    class Black_Jack
    {
        int selectedCardIndex = 0;
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
            string buf = "";
            d.score = 0;
            p.score = 0;

            foreach (Card card in d.hand)
            {
                if (card.number > 9)
                {
                    if (card.number != 14)
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
                    leaver_d = true;
                }
            }
            buf = "";
            Console.Write("\n\n\n\n\n\n");
            UserInterface.set_and_print("[]DEALER[]");
            foreach (Card card in d.hand)
            {
                buf += "{";
                buf += card.ToString();
                buf += "}";

            }
            buf += "(";
            buf += d.score;
            if (d.score2 > 0 && d.score2 < 22)
            {
                buf += "/";
                buf += d.score2;
            }
            buf += "p)";
            UserInterface.set_and_print(buf);
            UserInterface.set_and_print("*************");
            UserInterface.set_and_print("*             *");
            UserInterface.set_and_print("*               *");
            UserInterface.set_and_print("*                 *");
            UserInterface.set_and_print("*                 *");
            UserInterface.set_and_print("*               *");
            UserInterface.set_and_print("*             *");
            UserInterface.set_and_print("*************");
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
            buf = "";
            foreach (Card card in p.hand)
            {
                buf += "{";
                buf += card.ToString();
                buf += "}";

            }
            buf += "(";
            buf += p.score;
            if (p.score2 > 0 && p.score2 < 22)
            {
                buf += "/";
                buf += p.score2;
            }
            buf += "p)";
            UserInterface.set_and_print(buf);
            UserInterface.set_and_print("[]YOU[]");
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
                p.menu(selectedCardIndex);
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.Enter:
                        if (selectedCardIndex == 0)
                        {
                            p.hand.Add(deck[0]);
                            deck.Remove(deck[0]);
                            Console.Clear();
                            print_menu();
                            if (p.score > 21)
                            {
                                Console.WriteLine("YOU LOSE");
                                return;
                            }
                        }
                        else if (selectedCardIndex == 1)
                        {
                            Dealer_draw();
                            Check();
                            return;
                        }
                        else if (selectedCardIndex == 2)
                        {
                            //user.black_jack_b-=bid;
                            //bid*=2;
                            //p.hand.Add(deck[0]);
                            deck.Remove(deck[0]);
                            Console.Clear();
                            print_menu();
                            if (p.score > 21)
                            {
                                Console.WriteLine("YOU LOSE");
                                return;
                            }
                        }
                        break;

                    case ConsoleKey.RightArrow:

                        if (selectedCardIndex < 2)
                            selectedCardIndex++;

                        else
                            selectedCardIndex = 0;
                        break;
                    case ConsoleKey.LeftArrow:

                        if (selectedCardIndex > 0)
                            selectedCardIndex--;

                        else
                            selectedCardIndex = 2;
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
            if (p.score2 != 0 && p.score2 < 22)
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
            }
            else if (biggest_p == biggest_d)
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
        public void menu(int selectedCardIndex)
        {
            string buf = "DRAW  STOP  DOUBLE";
            string mask = "                  ";
            switch (selectedCardIndex)
            {
                case 0:
                    mask = "^^^^";
                    mask += "              ";
                    break;
                case 1:
                    mask = "      ";
                    mask += "^^^^";
                    mask += "        ";
                    break;
                case 2:
                    mask = "            ";
                    mask += "^^^^^^";
                    break;
            }
            Console.WriteLine();
            UserInterface.set_and_print(buf);
            UserInterface.set_and_print(mask);
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
