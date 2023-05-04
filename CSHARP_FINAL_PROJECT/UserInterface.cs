using BJ;
using Durak;
using YonatanMankovich.SimpleConsoleMenus;
using System.Runtime.Serialization.Formatters.Binary;
#pragma warning disable SYSLIB0011

static class UserInterface
{
    private static int WIDTH = Console.WindowWidth;
    internal delegate void print_delegate(object? obj);

    internal static void set_and_print(string[] elements, print_delegate? d = null, int step = 0)
    {
        int left;

        foreach (string element in elements)
        {
            left = (WIDTH / 2) - ((element.Length + step) / 2);
            try
            {
                Console.SetCursorPosition(left, Console.CursorTop);
            }
            catch
            { }
            finally
            {
                if (d != null)
                {
                    d(element);
                }

                else
                {
                    Console.WriteLine(element);
                }
            }
        }
    }

    internal static void set_and_print(string element, print_delegate? d = null, int step = 0)
    {
        int left = (WIDTH / 2) - ((element.Length + step) / 2);

        try
        {
            Console.SetCursorPosition(left, Console.CursorTop);
        }
        catch
        { }
        finally
        {
            if (d != null)
            {
                d(element);
            }

            else
            {
                Console.WriteLine(element);
            }
        }
    }

    internal static bool processed_input(ref string input)
    {
        ConsoleKeyInfo keyInfo;

        while ((keyInfo = Console.ReadKey()).Key != ConsoleKey.Enter)
        {
            if (keyInfo.Key == ConsoleKey.Backspace)
            {
                if (!string.IsNullOrEmpty(input))
                {
                    input = input.Remove(input.Length - 1);
                    Console.Write(" ");
                    Console.Write("\b \b");
                }
                else
                {
                    Console.Write(" ");
                }
                continue;
            }

            else if (keyInfo.Key == ConsoleKey.DownArrow) 
            {
                input = string.Empty;
                return false;
            }

            else
            {
                input += keyInfo.KeyChar;
            }
        }

        return true;
    }

    internal static void print_durak_logo() 
    {
        string text1 = "  ____                           _    ";
        string text2 = " |  _ \\   _   _   _ __    __ _  | | __";
        string text3 = " | | | | | | | | | '__|  / _` | | |/ /";
        string text4 = " | |_| | | |_| | | |    | (_| | |   < ";
        string text5 = " |____/   \\__,_| |_|     \\__,_| |_|\\_\\";

        set_and_print(new[]{ text1, text2, text3, text4, text5 });
    }

    internal static void print_bj_logo()
    {
        string text1 = "  ____    _                  _          _                  _    ";
        string text2 = " | __ )  | |   __ _    ___  | | __     | |   __ _    ___  | | __";
        string text3 = " |  _ \\  | |  / _` |  / __| | |/ /  _  | |  / _` |  / __| | |/ /";
        string text4 = " | |_) | | | | (_| | | (__  |   <  | |_| | | (_| | | (__  |   < ";
        string text5 = " |____/  |_|  \\__,_|  \\___| |_|\\_\\  \\___/   \\__,_|  \\___| |_|\\_\\";

        set_and_print(new[] { text1, text2, text3, text4, text5 });
    }

    internal static void print_memory_logo()
    {
        string text1 = "  __  __                                           ";
        string text2 = " |  \\/  |   ___   _ __ ___     ___    _ __   _   _ ";
        string text3 = " | |\\/| |  / _ \\ | '_ ` _ \\   / _ \\  | '__| | | | |";
        string text4 = " | |  | | |  __/ | | | | | | | (_) | | |    | |_| |";
        string text5 = " |_|  |_|  \\___| |_| |_| |_|  \\___/  |_|     \\__, |";
        string text6 = "                                             |___/ ";

        set_and_print(new[] { text1, text2, text3, text4, text5, text6 });
    }
    internal static void print_menu_logo() 
    {
        string text1 = "  __  __   _____   _   _   _   _ ";
        string text2 = " |  \\/  | | ____| | \\ | | | | | |";
        string text3 = " | |\\/| | |  _|   |  \\| | | | | |";
        string text4 = " | |  | | | |___  | |\\  | | |_| |";
        string text5 = " |_|  |_| |_____| |_| \\_|  \\___/ ";

        set_and_print(new[] { text1, text2, text3, text4, text5 });
    }

    internal static void print_info_logo()
    {
        string text1 = "  ___   _   _   _____    ___    ____    __  __      _      _____   ___    ___    _   _ ";
        string text2 = " |_ _| | \\ | | |  ___|  / _ \\  |  _ \\  |  \\/  |    / \\    |_   _| |_ _|  / _ \\  | \\ | |";
        string text3 = "  | |  |  \\| | | |_    | | | | | |_) | | |\\/| |   / _ \\     | |    | |  | | | | |  \\| |";
        string text4 = "  | |  | |\\  | |  _|   | |_| | |  _ <  | |  | |  / ___ \\    | |    | |  | |_| | | |\\  |";
        string text5 = " |___| |_| \\_| |_|      \\___/  |_| \\_\\ |_|  |_| /_/   \\_\\   |_|   |___|  \\___/  |_| \\_|";

        set_and_print(new[] { text1, text2, text3, text4, text5 });
    }

    internal static void print_about_us()
    {
        string text1 = "     _      _                       _       _   _       ";
        string text2 = "    / \\    | |__     ___    _   _  | |_    | | | |  ___ ";
        string text3 = "   / _ \\   | '_ \\   / _ \\  | | | | | __|   | | | | / __|";
        string text4 = "  / ___ \\  | |_) | | (_) | | |_| | | |_    | |_| | \\__ \\";
        string text5 = " /_/   \\_\\ |_.__/   \\___/   \\__,_|  \\__|    \\___/  |___/";

        set_and_print(new[] { text1, text2, text3, text4, text5 });
    }

    static void print_sign_logo()
    {
        string text1 = "  ____    _                     _                __    ____    _                                   ";
        string text2 = " / ___|  (_)   __ _   _ __     (_)  _ __        / /   / ___|  (_)   __ _   _ __      _   _   _ __  ";
        string text3 = " \\___ \\  | |  / _` | | '_ \\    | | | '_ \\      / /    \\___ \\  | |  / _` | | '_ \\    | | | | | '_ \\ ";
        string text4 = "  ___) | | | | (_| | | | | |   | | | | | |    / /      ___) | | | | (_| | | | | |   | |_| | | |_) |";
        string text5 = " |____/  |_|  \\__, | |_| |_|   |_| |_| |_|   /_/      |____/  |_|  \\__, | |_| |_|    \\__,_| | .__/ ";
        string text6 = "              |___/                                                |___/                    |_|    ";

        set_and_print(new[] { text1, text2, text3, text4, text5, text6 });
    }

    internal static void loading() 
    {
        string text = "Loading ";
        set_and_print(text, Console.Write, 20);
        string x = "█";
        for (int i = 0; i < 18; i++)
        {
            Console.Write(x);
            if(i < 7) Thread.Sleep(400);
            if (i >= 7 && i < 13) Thread.Sleep(250);
            if (i >= 13) Thread.Sleep(75);
        }
    }


    internal static void balance_print(long num) 
    {
        set_and_print(Convert.ToString(num), Console.Write);
    }

    internal static void main_menu(User us) 
    {
        IEnumerable<string> options = new List<string>() { "Durak   ", "BlackJack", "Memory  ", "Info    ", "About us","Exit    " };
        SimpleConsoleMenu menu = new SimpleConsoleMenu("Choose an option:", options);


        IEnumerable<string> options_1 = new List<string>() { "24  ", "32  ", "36  ", "52  ", "Exit" };
        SimpleConsoleMenu decks = new SimpleConsoleMenu("Choose deck:", options_1);

        IEnumerable<string> options_2 = new List<string>() { "Start", "Exit " };
        SimpleConsoleMenu otherg = new SimpleConsoleMenu("Choose an option:", options_2);

        IEnumerable<string> bJ = new List<string>() { "Start  ", "Balance", "Exit   " };
        SimpleConsoleMenu bj_menu = new SimpleConsoleMenu("Choose an option:", bJ);

        SimpleConsoleMenu about = new SimpleConsoleMenu("                ", "Exit ");

        IEnumerable<string> info_op = new List<string>() { "Durak   ", "BlackJack", "Memory  ", "Exit    " };
        SimpleConsoleMenu info = new SimpleConsoleMenu("Choose an option:", info_op);

        while (true)
        {
            print_menu_logo();
            Console.WriteLine("\n\n\n\n\n\n\n");

            menu.Show();
            Game game;
            bool run = true;
            switch (menu.SelectedIndex)
            {
                case 0:
                    while (run)
                    {
                        Console.Clear();
                        Console.WriteLine();
                        print_durak_logo();
                        Console.WriteLine("\n\n\n\n\n\n");

                        decks.Show();

                        switch (decks.SelectedIndex)
                        {
                            case 0:
                                Console.Clear();
                                Console.WriteLine();
                                print_durak_logo();
                                Console.WriteLine("\n\n\n");
                                loading();


                                game = new(9, new(us, new()), new());
                                game.Start();
                                Console.ReadKey();
                                break;
                            case 1:
                                Console.Clear();
                                Console.WriteLine();
                                print_durak_logo();
                                Console.WriteLine("\n\n\n");
                                loading();


                                game = new(7, new(us, new()), new());
                                game.Start();
                                Console.ReadKey();
                                break;
                            case 2:
                                Console.Clear();
                                Console.WriteLine();
                                print_durak_logo();
                                Console.WriteLine("\n\n\n");
                                loading();


                                game = new(6, new(us, new()), new());
                                game.Start();

                                Console.ReadKey();

                                break;

                            case 3:
                                Console.Clear();
                                Console.WriteLine();
                                print_durak_logo();
                                Console.WriteLine("\n\n\n");
                                loading();


                                game = new(2, new(us, new()), new());
                                game.Start();
                                Console.ReadKey();
                                break;
                            case 4:
                                run = false;
                                Console.Clear();
                                break;
                        }
                    }
                    break;
                case 1:
                    while (run)
                    {
                        Console.Clear();
                        Console.WriteLine();
                        print_bj_logo();
                        Console.WriteLine("\n\n\n\n\n\n");

                        bj_menu.Show();

                        switch (bj_menu.SelectedIndex)
                        {
                            case 0:
                                Console.Clear();
                                Console.WriteLine();
                                print_bj_logo();
                                Console.WriteLine("\n\n\n");
                                loading();
                                Black_Jack bj = new(us);
                                bj.BJ_init();
                                Console.ReadKey();  
                                break;

                            case 1:
                                Console.Clear();
                                Console.WriteLine();
                                print_bj_logo();
                                Console.WriteLine("\n\n\n\n\n");

                                set_and_print("Balance: ", Console.Write);
                                Console.Write(us.black_jack_b);
                                Console.ReadKey();
                                break;
                            case 2:
                                run = false;
                                Console.Clear();
                                break;
                        }
                    }
                    break;
                case 2:
                    while (run)
                    {
                        Console.Clear();
                        Console.WriteLine();
                        print_memory_logo();
                        Console.WriteLine("\n\n\n\n\n\n");

                        otherg.Show();

                        switch (otherg.SelectedIndex)
                        {
                            case 0:
                                Console.Clear();
                                Console.WriteLine();
                                print_memory_logo();
                                Console.WriteLine("\n\n\n");
                                loading();
                                MM.Memory m = new();
                                m.game();
                                break;
                            case 1:
                                run = false;
                                Console.Clear();
                                break;
                        }
                    }
                    break;


                case 4:
                    while (run)
                    {
                        Console.Clear();
                        Console.WriteLine();
                        print_about_us();
                        Console.WriteLine("\n\n\n");

                        set_and_print("Programers:");
                        set_and_print("Prosto_Alex : 5375411417903080");
                        set_and_print("Tanyok : 4149499159434339     ");
                        set_and_print("4elovel : -                   ");
                        set_and_print("wyleesss : -                  ");
                        Console.WriteLine();
                        set_and_print("Story:");
                        set_and_print("Hello everyone, I'm Alex, I'm 16 and this is our little project. I am a leader, and I can say that I am extremely");
                        set_and_print("glad that I have such a team. I repeatedly convinced myself that these are really the coolest people there are,  ");
                        set_and_print("sometimes there were thoughts that I was not worthy to be a commander, because my team did such things that you  ");
                        set_and_print("could not even dream of. What you see in front of you is our little project, into which we have poured our hearts");
                        set_and_print("all to the last, which is only worth the sleepless nights we gave to create it.                                  ");
                        set_and_print("Hope you like it and enjoy it, thanks to all. Alex.                                                              ");
                        Console.WriteLine();
                        Console.WriteLine();
                        about.Show();
                        switch(about.SelectedIndex) 
                        {
                            case 0:
                                run = false;
                                Console.Clear();
                                break;
                        }
                    }
                    break;
                case 3:
                    while (run)
                    {
                        Console.Clear();
                        Console.WriteLine();
                        print_info_logo();
                        Console.WriteLine("\n\n\n\n\n\n");
                        info.Show();
                        switch(info.SelectedIndex) 
                        {
                            case 0:
                                Console.Clear();
                                Console.WriteLine("\n\n\n\n\n\n");
                                set_and_print("“Durak” is a card game whose goal is to get rid of all of its cards by staying the opponent`s cards higher.       ");

                                set_and_print("The rules of the game in the \"fool\" are quite simple.                                                             ");
                                set_and_print("Each player gets 6 cards, the remaining cards remain in the pile.                                                  ");
                                set_and_print("The peculiarity of the game is that the kozir (suit, which in this game has the most of all colors)                ");
                                set_and_print("changes in each round.                                                                                             ");
                                set_and_print("Each player turns, throwing cards on the table, on which the other player should put                               ");
                                set_and_print("a card higher than the same suit, or a Trump card of any suit, if the table already has a Trump card.              ");
                                set_and_print("If the player cannot stay the card, he must remove all cards from the table and add them to his own.               ");
                                set_and_print("In a Durak , the right and left arrows are also used to move, the up arrow is used to select an action,            ");
                                set_and_print("the down arrow is used to end the move, finish adding cards, or if it is impossible to beat (to pick up cards).    ");
                                Console.ReadKey();
                                break;
                            case 1: 
                                Console.Clear();
                                Console.WriteLine("\n\n\n\n\n\n");
                                set_and_print("\"Blackjack\" is a gamble where the player is competing against the dealer.                                        ");
                                set_and_print("The game is to score more points than the dealer, not exceeding 21.                                              ");
                                set_and_print("Each player gets two cards first, and the dealer gets one card.  Each card has its rank and points.              ");
                                set_and_print("Cards with numbers have a rank from 2 to 10, the face (including king, dama and valeta) have a rank of 10.       ");
                                set_and_print("the ACE can have a rank of 1 or 11, depending on what is more advantageous for the player.                       ");
                                set_and_print("The player can pick up additional cards (\"draw\") until the sum of points does not exceed 21                      ");
                                set_and_print("or decide that they have already scored enough points.                                                           ");
                                set_and_print("If the player's score exceeds 21, it will automatically lose (\"over - taken\").                                   ");
                                set_and_print("After all the players have decided that he is ready, the dealer adds the cards.                                  ");
                                set_and_print("If the dealer's points amount is less than 17, he must take another card,                                        ");
                                set_and_print("and if the sum of points 17 or more, the dealer stops.                                                           ");
                                set_and_print("The player wins if his points are more than the dealer, not exceeding 21.                                        ");
                                set_and_print("If the score of players and dealer is the same, the game is considered a night.                                  ");
                                Console.ReadKey();
                                break;

                            case 2:
                                Console.Clear();
                                Console.WriteLine("\n\n\n\n\n\n");
                                set_and_print("\"Memory\" is a game where players can test their memory by trying to find a pair of cards.                        ");
                                set_and_print("The goal of the game is to open 16 pairs of cards.                                                               ");
                                set_and_print("The cards are facing up and need to be wrapped to find a pair.                                                   ");
                                set_and_print("If the cards you turn do not match, they will turn upside down.                                                  ");
                                set_and_print("and the player should continue to search for a pair.                                                             ");
                                set_and_print("The game ends when the player finds all the pairs of pictures.                                                   ");
                                set_and_print("After the game is over, the player gets a final score, which depends on the number of moves made by the player.  ");
                                set_and_print("To select a card, you need to enter the number of its trunk, then the line number through the comma.             ");
                                Console.ReadKey();
                                break;
                            case 3:
                                run = false;
                                Console.Clear();
                                break;
                        }
                    }
                    break;
                case 5:
                    Console.Clear(); //exit
                    BinaryFormatter formatter = new();
                    List<User> users = new List<User>();

                    using (Stream fStream = File.OpenRead("..\\..\\..\\DB.bin"))
                    {
                        users = (List<User>)formatter.Deserialize(fStream);
                    }

                    for (int i = 0; i < users.Count; i++)
                    {
                        if (users[i].login == us.login && users[i].password == us.password)
                        {
                            users[i] = us;
                        }
                    }
                    using (var st = new FileStream("..\\..\\..\\DB.bin", FileMode.Create, FileAccess.Write))
                    {
                        formatter.Serialize(st, users);
                    }

                    Environment.Exit(0);
                    break; 
            }
        }
    }

    internal static User sign_menu()
    {
        Console.Title = "ЄХЯЗ";
        string input_user_name = string.Empty;
        string input_login = string.Empty;
        string input_password = string.Empty;

        IEnumerable<string> options = new List<string>() { "Sing in", "Sign up", "Exit   " };
        SimpleConsoleMenu menu = new SimpleConsoleMenu("Choose an option:", options);

        while (true)
        {
            print_sign_logo();
            Console.WriteLine("\n\n\n\n\n\n\n");

            menu.Show();

            switch (menu.SelectedIndex)
            {
                case 0:

                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine("[↓] - PREVIOUS PAGE\n\n\n\n\n\n\n\n\n\n");

                        set_and_print("input account data\n");
                        set_and_print("Login: ", Console.Write);
                        if (!processed_input(ref input_login))
                        {
                            Console.Clear();
                            break;
                        }

                        Console.WriteLine();

                        set_and_print("Password: ", Console.Write);
                        if (!processed_input(ref input_password))
                        {
                            Console.Clear();
                            break;
                        }

                        if (!SerialDB.check(input_login, input_password))
                        {
                            Console.Clear();
                            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n");
                            set_and_print("INCORRECT LOGIN/PASSWORD");
                            input_login = string.Empty;
                            input_password = string.Empty;
                            Thread.Sleep(2000);
                            Console.Clear();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n");
                            set_and_print("SING IN CONFIRMED");
                            Thread.Sleep(2000);
                            Console.Clear();
                            return SerialDB.take(input_login, input_password);
                        }
                    }

                    break;

                case 1:

                    Console.Clear();
                    Console.WriteLine("[↓] - PREVIOUS PAGE\n\n\n\n\n\n\n\n\n\n");

                    set_and_print("create an account\n");
                    set_and_print("User Name: ", Console.Write);
                    if (!processed_input(ref input_user_name))
                    {
                        Console.Clear();
                        break;
                    }

                    Console.WriteLine();

                    set_and_print("Login: ", Console.Write);
                    if (!processed_input(ref input_login))
                    {
                        Console.Clear();
                        break;
                    }

                    if (!SerialDB.reg_check(input_login))
                    {
                        Console.Clear();
                        Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n");
                        set_and_print("LOGIN IS ALREADY EXIST");
                        Thread.Sleep(2000);
                        Console.Clear();
                        break;
                    }

                    Console.WriteLine();

                    set_and_print("Password: ", Console.Write);
                    if (!processed_input(ref input_password))
                    {
                        Console.Clear();
                        break;
                    }

                    Console.Clear();

                    Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n");
                    set_and_print("SIGN UP CONFIRMED");
                    Thread.Sleep(2000);
                    Console.Clear();

                    User user = new(input_user_name, input_login, input_password, 1000, 1000, 5, new(), new());
                    SerialDB.push(user);

                    return user;

                case 2:
                    Console.Clear();
                    Environment.Exit(0);
                    break;
            }
        }
    }
}