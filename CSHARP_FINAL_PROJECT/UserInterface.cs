using BJ;
using Durak;
using Microsoft.CSharp.RuntimeBinder;
using System.ComponentModel;
using System.Xml.Linq;
using YonatanMankovich.SimpleConsoleMenus;

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
            Console.SetCursorPosition(left, Console.CursorTop);

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

    internal static void set_and_print(string element, print_delegate? d = null, int step = 0)
    {
        int left = (WIDTH / 2) - ((element.Length + step) / 2);
        Console.SetCursorPosition(left, Console.CursorTop);

        if (d != null)
        {
            d(element);
        }

        else
        {
            Console.WriteLine(element);
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
        set_and_print(text, Console.Write, 10);
        string x = "█";
        for (int i = 0; i < 25; i++)
        {
            Console.Write(x);
            if(i < 10) Thread.Sleep(400);
            if (i >= 10 && i < 20) Thread.Sleep(250);
            if (i >= 20) Thread.Sleep(75);
        }
    }

    internal static void main_menu(User us) 
    {
        IEnumerable<string> options = new List<string>() { "Durak   ", "BlackJack", "Memory  ","How use ","Exit    " };
        SimpleConsoleMenu menu = new SimpleConsoleMenu("Choose an option:", options);


        IEnumerable<string> options_1 = new List<string>() { "24  ", "32  ", "36  ", "52  ", "Exit" };
        SimpleConsoleMenu decks = new SimpleConsoleMenu("Choose deck:", options_1);

        IEnumerable<string> options_2 = new List<string>() { "Start", "Exit " };
        SimpleConsoleMenu otherg = new SimpleConsoleMenu("Choose an option:", options_2);

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


                                game = new(9, new(us.user_name, new()), new());
                                game.Start();

                                break;
                            case 1:
                                Console.Clear();
                                Console.WriteLine();
                                print_durak_logo();
                                Console.WriteLine("\n\n\n");
                                loading();


                                game = new(7, new(us.user_name, new()), new());
                                game.Start();

                                break;
                            case 2:
                                Console.Clear();
                                Console.WriteLine();
                                print_durak_logo();
                                Console.WriteLine("\n\n\n");
                                loading();


                                game = new(6, new(us.user_name, new()), new());
                                game.Start();

                                break;

                            case 3:
                                Console.Clear();
                                Console.WriteLine();
                                print_durak_logo();
                                Console.WriteLine("\n\n\n");
                                loading();


                                game = new(2, new(us.user_name, new()), new());
                                game.Start();

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

                        otherg.Show();

                        switch (otherg.SelectedIndex)
                        {
                            case 0:
                                Console.Clear();
                                Console.WriteLine();
                                print_bj_logo();
                                Console.WriteLine("\n\n\n");
                                loading();
                                Black_Jack bj = new();
                                bj.BJ_init();
                                break;
                            case 1:
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
                                m.go();
                                break;
                            case 1:
                                run = false;
                                Console.Clear();
                                break;
                        }
                    }
                    break;
                case 4:
                    Console.Clear();
                    Environment.Exit(0);
                    break; 
            }
        }
    }

    internal static User sign_menu()
    {
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

                            Thread.Sleep(2000);
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n");
                            set_and_print("SING IN CONFIRMED");

                            Thread.Sleep(2000);
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

                    User user = new(input_user_name, input_login, input_password, 0, 0, 5, new(), new());
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