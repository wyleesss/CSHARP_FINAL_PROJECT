namespace Durak
{
    class Player
    {
        public string name { get; }
        public List<Card> koloda { get; set; } = new();
        public char koz { get; set; }
        public List<SpecialAbilities> sa { get; }
        public bool is_playing { get; set; } = false;

        public Player(string name, List<SpecialAbilities> sa)
        {
            this.name = name;
            this.sa = sa;
        }

        public void print_cards()
        {
            foreach (Card card in koloda)
                Console.Write($"{{{card, -3}}} ");
        }

        public int attack(List<Card> game_table, print_info info)
        {
            ConsoleKeyInfo keyInfo = new();
            int selectedCardIndex = 0;

            while (true)
            {
                Console.Clear();
                info();

                Console.Write("\n");

                for (int i = 0; i <= selectedCardIndex; i++)
                {
                    if (i == selectedCardIndex)
                    {
                        if (koloda[selectedCardIndex].ToString().Length == 3)
                            Console.Write(" ^^^ ");
                        else
                            Console.Write(" ^^  ");
                    }

                    else
                        Console.Write("      ");
                }

                Console.Write("\n");

                keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (selectedCardIndex > 0)
                            selectedCardIndex--;
                        break;

                    case ConsoleKey.RightArrow:
                        if (selectedCardIndex < koloda.Count - 1)
                            selectedCardIndex++;
                        break;

                    case ConsoleKey.Enter:
                        if (game_table.Count == 0)
                            return selectedCardIndex;

                        else if (game_table.Count <= 12)
                        {
                            foreach (Card card in game_table)
                            {
                                if (koloda[selectedCardIndex].number == card.number)
                                    return selectedCardIndex;
                            }

                            break;
                        }

                        break;

                    case ConsoleKey.DownArrow:
                        if (game_table.Count > 0)
                            return -1;

                        else
                            break;

                    default:
                        Console.Write("\b \b");
                        break;
                }
            }
        }

        public void additional_attack(List<Card> game_table, print_info info) ///////////////////////////////////////////////////
        { }

        public int defense(Card att, print_info info)
        {
            ConsoleKeyInfo keyInfo = new();
            int selectedCardIndex = 0;

            while (true)
            {
                Console.Clear();
                info();

                Console.Write("\n");

                for (int i = 0; i <= selectedCardIndex; i++)
                {
                    if (i == selectedCardIndex)
                    {
                        if (koloda[selectedCardIndex].ToString().Length == 3)
                            Console.Write(" ^^^ ");
                        else
                            Console.Write(" ^^  ");
                    }

                    else
                        Console.Write("      ");
                }

                Console.Write("\n");

                keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (selectedCardIndex > 0)
                            selectedCardIndex--;
                        break;

                    case ConsoleKey.RightArrow:
                        if (selectedCardIndex < koloda.Count - 1)
                            selectedCardIndex++;
                        break;

                    case ConsoleKey.Enter:
                        if (koloda[selectedCardIndex].suit == koz && att.suit != koz)
                            return selectedCardIndex;

                        else if (koloda[selectedCardIndex].suit == att.suit && koloda[selectedCardIndex].number > att.number)
                            return selectedCardIndex;

                        else
                            break;

                    case ConsoleKey.DownArrow:
                        return -1;

                    default:
                        Console.Write("\b \b");
                        break;
                }
            }
        }
    }
}