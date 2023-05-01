// можливість докинути карти коли бот бере (не за щоку)
// параметри гри (?)
// рефакторинг

namespace Durak
{
    using System.Threading;
    delegate void print_info();

    class Game
    {
        Player player;
        BOTIK_BATON bb;
        List<Card> koloda;
        List<Card> rebound = new();
        char trump_suit;

        List<Card> game_table = new();

        public Game(int min_card, Player player, BOTIK_BATON bb)
        {
            this.player = player;
            this.bb = bb;

            koloda = Card.Schuffle(min_card);
            koloda = Card.Mix(koloda);
            trump_suit = koloda[koloda.Count - 1].suit;

            bb.koz = trump_suit;
            player.koz = trump_suit;

            Random random = new();

            if (random.Next(1, 14) % 2 == 0)
            {
                player.is_playing = !(bb.is_playing = true);
                give_cards_bbpr();
            }

            else
            {
                bb.is_playing = !(player.is_playing = true);
                give_cards_plpr();
            }

            sort_cards();
        }

        public void give_cards_plpr()
        {
            try
            {
                int player_need = 6 - player.koloda.Count;

                for (int i = 0; i < player_need; i++)
                {
                    player.koloda.Add(koloda[koloda.Count - 1]);
                    koloda.RemoveAt(koloda.Count - 1);
                }

                int bot_need = 6 - bb.koloda.Count;

                for (int i = 0; i < bot_need; i++)
                {
                    bb.koloda.Add(koloda[koloda.Count - 1]);
                    koloda.RemoveAt(koloda.Count - 1);
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                return;
            }
        }

        public void give_cards_bbpr()
        {
            try
            {
                int bot_need = 6 - bb.koloda.Count;

                for (int i = 0; i < bot_need; i++)
                {
                    bb.koloda.Add(koloda[koloda.Count - 1]);
                    koloda.RemoveAt(koloda.Count - 1);
                }

                int player_need = 6 - player.koloda.Count;

                for (int i = 0; i < player_need; i++)
                {
                    player.koloda.Add(koloda[koloda.Count - 1]);
                    koloda.RemoveAt(koloda.Count - 1);
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                return;
            }
        }

        public void sort_cards()
        {
            player.koloda = Card.SortNum(player.koloda);
            player.koloda = Card.SortSuit(player.koloda);
            player.koloda = Card.SortSuit(player.koloda, trump_suit);

            bb.koloda = Card.SortNum(bb.koloda);
            bb.koloda = Card.SortSuit(bb.koloda);
            bb.koloda = Card.SortSuit(bb.koloda, trump_suit);
        }

        public void print_game_table()
        {
            for (int i = 0; i < game_table.Count; i++)
            {
                if ((i + 1) % 4 == 0)
                {
                    Console.Write(" ×<- ");
                    Console.Write($"{{{game_table[i],-3}}}");
                    Console.Write("\n");
                }
                else if ((i + 1) % 2 == 0)
                {
                    Console.Write(" ×<- ");
                    Console.Write($"{{{game_table[i],-3}}}");
                    Console.Write("   ");
                }
                else
                {
                    Console.Write($"{{{game_table[i],-3}}}");
                }
            }

            Console.Write("\n");
        }

        public void print_info()
        {
            Console.Write("OPPONENT:" + (bb.is_playing ? " (attacking)\n" : " (defending)\n") +
                         $"\"bot::BATON\" [cards:: ({bb.koloda.Count})]\n\n\n\n");

            if (game_table.Count == 0)
                Console.Write("\n\n\n\n");

            else if (game_table.Count <= 3)
            {
                print_game_table();
                Console.Write("\n\n\n");
            }
            else if (game_table.Count <= 7)
            {
                print_game_table();
                Console.Write("\n\n");
            }
            else
            {
                print_game_table();
                Console.Write("\n");
            }

            Console.Write("\nTS - '" + trump_suit.ToString() + "'" +
                         $"\ncards left - {koloda.Count}\n" +
                         (player.is_playing ? "| ATTACK! |\n\n\n" : "| DEFEND! |\n\n\n"));

            Console.Write("\nYOU:" + (player.is_playing ? " (attacking)\n" : " (defending)\n") +
                         $"\"{player.name}\" [cards:: ({player.koloda.Count})]\n\n");

            player.print_cards();
        }

        public void ConsoleUpdate(string message, int ms = 1500)
        {
            Console.Clear();
            Console.WriteLine(message + "\n");
            print_info();
            Console.Write("\n");
            Thread.Sleep(ms);
        }

        public void mixing()
        {
            koloda = Card.Mix(rebound);
            rebound.Clear();
            trump_suit = koloda[koloda.Count - 1].suit;
        }

        public void Start()
        {
            bool first_rebound = true;
            int moves_it = 0;

            while (!((koloda.Count == 0 && player.koloda.Count == 0) || (koloda.Count == 0 && bb.koloda.Count == 0)))
            {
                if (player.is_playing)
                {
                    if (first_rebound && moves_it == 10)
                    {
                        ConsoleUpdate("~~~~~~~[END OF THE ATTACK]~~~~~~~", 2500);

                        first_rebound = false;
                        moves_it = 0;

                        foreach (var card in game_table)
                            rebound.Add(card);

                        game_table.Clear();
                        give_cards_plpr();
                        sort_cards();

                        player.is_playing = !(bb.is_playing = true);
                        continue;
                    }
                    else if (moves_it == 12)
                    {
                        ConsoleUpdate("~~~~~~~[END OF THE ATTACK]~~~~~~~", 2500);

                        moves_it = 0;

                        foreach (var card in game_table)
                            rebound.Add(card);

                        game_table.Clear();
                        give_cards_plpr();
                        sort_cards();

                        player.is_playing = !(bb.is_playing = true);
                        continue;
                    }

                    var att = player.attack(game_table, print_info);

                    if (att != -1)
                    {
                        moves_it++;

                        game_table.Add(player.koloda[att]);
                        player.koloda.RemoveAt(att);

                        ConsoleUpdate("×××××××[OPPONENT MOVE]×××××××");

                        if (koloda.Count == 0 && player.koloda.Count == 0)
                        {
                            ConsoleUpdate("+++++++[WIN]+++++++", 0);
                            return;
                        }

                        var def = bb.defense(game_table[game_table.Count - 1]);

                        if (def != -1)
                        {
                            moves_it++;

                            game_table.Add(bb.koloda[def]);
                            bb.koloda.RemoveAt(def);

                            if (koloda.Count <= 6 && bb.koloda.Count == 0)
                            {
                                ConsoleUpdate("-------[DEFEAT]-------", 0);
                                return;
                            }


                            continue;
                        }
                        else
                        {
                            moves_it = 0;

                            ConsoleUpdate("~~~~~~~[OPPONENT TAKES CARDS]~~~~~~~", 2500);

                            foreach (var card in game_table)
                                bb.koloda.Add(card);

                            player.additional_attack(game_table, print_info);

                            game_table.Clear();
                            give_cards_plpr();
                            sort_cards();

                            continue;
                        }
                    }
                    else
                    {
                        if (first_rebound)
                            first_rebound = false;

                        moves_it = 0;

                        ConsoleUpdate("~~~~~~~[END OF THE ATTACK]~~~~~~~", 2500);

                        foreach (var card in game_table)
                            rebound.Add(card);

                        game_table.Clear();
                        give_cards_plpr();
                        sort_cards();

                        player.is_playing = !(bb.is_playing = true);
                    }
                }
                else
                {
                    if (first_rebound && moves_it == 10)
                    {
                        ConsoleUpdate("~~~~~~~[END OF THE ATTACK]~~~~~~~", 2500);

                        first_rebound = false;
                        moves_it = 0;

                        foreach (var card in game_table)
                            rebound.Add(card);

                        game_table.Clear();
                        give_cards_bbpr();
                        sort_cards();

                        bb.is_playing = !(player.is_playing = true);
                        continue;
                    }
                    else if (moves_it == 12)
                    {
                        ConsoleUpdate("~~~~~~~[END OF THE ATTACK]~~~~~~~", 2500);

                        moves_it = 0;

                        foreach (var card in game_table)
                            rebound.Add(card);

                        game_table.Clear();
                        give_cards_bbpr();
                        sort_cards();

                        bb.is_playing = !(player.is_playing = true);
                        continue;
                    }

                    ConsoleUpdate("×××××××[OPPONENT MOVE]×××××××");

                    var att = bb.attack(game_table);

                    if (att != -1)
                    {
                        moves_it++;

                        game_table.Add(bb.koloda[att]);
                        bb.koloda.RemoveAt(att);

                        if (koloda.Count == 0 && bb.koloda.Count == 0)
                        {
                            ConsoleUpdate("-------[DEFEAT]-------", 0);
                            return;
                        }

                        var def = player.defense(game_table[game_table.Count - 1], print_info);

                        if (def != -1)
                        {
                            moves_it++;

                            game_table.Add(player.koloda[def]);
                            player.koloda.RemoveAt(def);

                            if (koloda.Count <= 6 && player.koloda.Count == 0)
                            {
                                ConsoleUpdate("+++++++[WIN]++++++++", 0);
                                return;
                            }

                            continue;
                        }
                        else
                        {
                            ConsoleUpdate("~~~~~~~[YOU TAKE THE CARDS]~~~~~~~", 2500);

                            moves_it = 0;

                            foreach (var card in game_table)
                                player.koloda.Add(card);

                            game_table.Clear();
                            give_cards_bbpr();
                            sort_cards();

                            continue;
                        }
                    }
                    else
                    {
                        if (first_rebound)
                            first_rebound = false;

                        moves_it = 0;

                        ConsoleUpdate("~~~~~~~[END OF THE ATTACK]~~~~~~~", 2500);

                        foreach (var card in game_table)
                            rebound.Add(card);

                        game_table.Clear();
                        give_cards_bbpr();
                        sort_cards();

                        bb.is_playing = !(player.is_playing = true);
                    }
                }
            }

            if (player.koloda.Count == 0)
            {
                ConsoleUpdate("+++++++[WIN]++++++++", 0);
            }
            else
            {
                ConsoleUpdate("-------[DEFEAT]-------", 0);
            }

        }
    }
}