namespace Durak
{
    class Player
    {
        public User user { get; set; }

        public string name { get; }
        public List<Card> koloda { get; set; } = new();
        public char koz { get; set; }
        public List<SpecialAbilities> sa { get; }
        public bool is_playing { get; set; } = false;

        public Player(User user, List<SpecialAbilities> sa)
        {
            this.user = user;
            this.name = user.user_name;
            this.sa = sa;
        }

        public void print_cards()
        {
            if (koloda.Count == 0)
            {
                UserInterface.set_and_print("none");
                return;
            }

            string fullLine = string.Empty;

            foreach (Card card in koloda)
                fullLine += $"{{{card,-3}}} ";

            UserInterface.set_and_print(fullLine.Substring(0, fullLine.Length - 1));
        }

        public int attack(List<Card> game_table, print_info info)
        {
            ConsoleKeyInfo keyInfo = new();
            int selectedCardIndex = 0;
            string fullLine = string.Empty;

            while (true)
            {
                Console.Clear();
                UserInterface.set_and_print("×××××××[YOUR MOVE]×××××××");
                Console.Write("\n");
                info(false);

                for (int i = 0; i <= koloda.Count - 1; i++)
                {
                    if (i == selectedCardIndex)
                    {
                        if (koloda[selectedCardIndex].ToString().Length == 3)
                            fullLine += " ^^^ ";

                        else
                            fullLine += " ^^  ";
                    }

                    else
                        fullLine += "      ";
                }

                UserInterface.set_and_print(fullLine);
                Console.Write("\n");
                fullLine = string.Empty;

                keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.LeftArrow:

                        if (selectedCardIndex > 0)
                            selectedCardIndex--;

                        else
                            selectedCardIndex = koloda.Count - 1;

                        break;

                    case ConsoleKey.RightArrow:

                        if (selectedCardIndex < koloda.Count - 1)
                            selectedCardIndex++;

                        else
                            selectedCardIndex = 0;

                        break;

                    case ConsoleKey.UpArrow:

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

        public int additional_attack(List<Card> game_table, print_info info)
        {
            ConsoleKeyInfo keyInfo = new();
            int selectedCardIndex = 0;
            string fullLine = string.Empty;

            while (true)
            {
                Console.Clear();
                UserInterface.set_and_print("~~~~~~~[OPPONENT TAKES CARDS]~~~~~~~");
                UserInterface.set_and_print("       <<<   shed cards   >>>       ");
                info(true);

                for (int i = 0; i <= koloda.Count - 1; i++)
                {
                    if (i == selectedCardIndex)
                    {
                        if (koloda[selectedCardIndex].ToString().Length == 3)
                            fullLine += " ^^^ ";

                        else
                            fullLine += " ^^  ";
                    }

                    else
                        fullLine += "      ";
                }

                UserInterface.set_and_print(fullLine);
                Console.Write("\n");
                fullLine = string.Empty;

                keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.LeftArrow:

                        if (selectedCardIndex > 0)
                            selectedCardIndex--;

                        else
                            selectedCardIndex = koloda.Count - 1;

                        break;

                    case ConsoleKey.RightArrow:

                        if (selectedCardIndex < koloda.Count - 1)
                            selectedCardIndex++;

                        else
                            selectedCardIndex = 0;

                        break;

                    case ConsoleKey.UpArrow:

                        if (game_table.Count <= 12)
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
                        return -1;

                    default:
                        Console.Write("\b \b");
                        break;
                }
            }
        }

        public int defense(Card att, print_info info)
        {
            ConsoleKeyInfo keyInfo = new();
            int selectedCardIndex = 0;
            string fullLine = string.Empty;

            while (true)
            {
                Console.Clear();
                UserInterface.set_and_print("×××××××[YOUR MOVE]×××××××");
                Console.Write("\n");
                info(false);

                for (int i = 0; i <= koloda.Count - 1; i++)
                {
                    if (i == selectedCardIndex)
                    {
                        if (koloda[selectedCardIndex].ToString().Length == 3)
                            fullLine += " ^^^ ";

                        else
                            fullLine += " ^^  ";
                    }

                    else
                        fullLine += "      ";
                }

                UserInterface.set_and_print(fullLine);
                Console.Write("\n");
                fullLine = string.Empty;

                keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.LeftArrow:

                        if (selectedCardIndex > 0)
                            selectedCardIndex--;

                        else
                            selectedCardIndex = koloda.Count - 1;

                        break;

                    case ConsoleKey.RightArrow:

                        if (selectedCardIndex < koloda.Count - 1)
                            selectedCardIndex++;

                        else
                            selectedCardIndex = 0;

                        break;

                    case ConsoleKey.UpArrow:

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