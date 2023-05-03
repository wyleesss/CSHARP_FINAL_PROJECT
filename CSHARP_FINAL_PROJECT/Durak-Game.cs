namespace Durak
{
    delegate void print_info(bool expression);

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
            Random random = new();
            this.player = player;
            this.bb = bb;

            koloda = Card.Schuffle(min_card);
            koloda = Card.Mix(koloda);
            trump_suit = koloda[random.Next(0, koloda.Count)].suit;

            bb.koz = trump_suit;
            player.koz = trump_suit;

            if (random.Next(1, 100) % 2 == 0)
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
            List<string> fullLines = new();

            for (int i = 0; i < game_table.Count; i++)
            {
                if ((i + 1) % 2 == 0)
                {
                    fullLines[fullLines.Count - 1] += $" ×<- {{{game_table[i],-3}}}";
                }
                else if (i != 0 && i != 4 && i != 8)
                {
                    fullLines[fullLines.Count - 1] += $"   {{{game_table[i],-3}}}";
                }
                else
                {
                    fullLines.Add($"{{{game_table[i],-3}}}");
                }
            }

            UserInterface.set_and_print(fullLines.ToArray());
            Console.Write("\n");
        }

        public void print_shed_table()
        {
            List<string> fullLines = new();

            for (int i = 0; i < game_table.Count; i++)
            {
                if ((i + 1) % 2 == 0)
                {
                    fullLines[fullLines.Count - 1] += $"   {{{game_table[i],-3}}}";
                }
                else if (i != 0 && i != 4 && i != 8)
                {
                    fullLines[fullLines.Count - 1] += $"   {{{game_table[i],-3}}}";
                }
                else
                {
                    fullLines.Add($"{{{game_table[i],-3}}}");
                }
            }

            UserInterface.set_and_print(fullLines.ToArray());
            Console.Write("\n");
        }

        public void print_info(bool is_shedding)
        {
            UserInterface.set_and_print("OPPONENT:" + (bb.is_playing ? " (attacking)" : " (defending)"));
            UserInterface.set_and_print($"\"bot::BATON\" [cards:: ({bb.koloda.Count})]");
            Console.Write("\n\n\n");

            if (game_table.Count == 0)
                Console.Write("\n\n\n\n\n\n\n");

            else if (game_table.Count <= 4)
            {
                if (is_shedding)
                    print_shed_table();

                else
                    print_game_table();

                Console.Write("\n\n\n\n\n");
            }
            else if (game_table.Count <= 8)
            {
                if (is_shedding)
                    print_shed_table();

                else
                    print_game_table();

                Console.Write("\n\n\n\n");
            }
            else
            {
                if (is_shedding)
                    print_shed_table();

                else
                    print_game_table();

                Console.Write("\n\n\n");
            }

            UserInterface.set_and_print($"TS -- '{trump_suit}'");
            Console.Write("\n");
            UserInterface.set_and_print($"cards left - {koloda.Count}");
            Console.Write("\n");
            UserInterface.set_and_print(player.is_playing ? "| ATTACK! |" : "| DEFEND! |");

            Console.Write("\n\n\n");

            UserInterface.set_and_print($"YOU:" + (player.is_playing ? " (attacking)" : " (defending)"));
            UserInterface.set_and_print($"\"{player.name}\" [cards:: ({player.koloda.Count})]\n");

            Console.Write("\n");

            player.print_cards();
        }

        private void ConsoleUpdate(string message, int ms = 1500, bool is_shedding = false)
        {
            Console.Clear();

            UserInterface.set_and_print(message);
            Console.Write("\n");

            print_info(is_shedding);
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

                        var def = bb.defense(game_table[game_table.Count - 1], game_table.Count, koloda.Count);

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

                            int add;
                            int counter = 0;

                            while ((first_rebound && game_table.Count <= 10) || (!first_rebound && game_table.Count <= 12))
                            {
                                if (first_rebound)
                                {
                                    if (counter == 5)
                                        break;
                                }
                                else
                                {
                                    if (counter == 6)
                                        break;
                                }

                                add = player.additional_attack(game_table, print_info);

                                if (add == -1)
                                    break;

                                game_table.Add(player.koloda[add]);
                                player.koloda.RemoveAt(add);

                                counter++;
                            }

                            foreach (var card in game_table)
                                bb.koloda.Add(card);

                            ConsoleUpdate("~~~~~~~[END OF THE ATTACK]~~~~~~~", 2500, true);
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

                    var att = bb.attack(game_table, koloda.Count);

                    if (att != -1)
                    {
                        moves_it++;

                        game_table.Add(bb.koloda[att]);
                        bb.koloda.RemoveAt(att);

                        if (koloda.Count == 0 && bb.koloda.Count == 0)
                        {
                            ConsoleUpdate("-------[DEFEAT]-------", 0);
                            Console.ReadKey();
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
                                Console.ReadKey();
                                return;
                            }

                            continue;
                        }
                        else
                        {
                            int add;
                            int counter = 0;

                            ConsoleUpdate("~~~~~~~[YOU TAKE THE CARDS]~~~~~~~", 1500, true);

                            while ((first_rebound && game_table.Count <= 10) || (!first_rebound && game_table.Count <= 12))
                            {
                                if (first_rebound)
                                {
                                    if (counter == 5)
                                        break;
                                }
                                else
                                {
                                    if (counter == 6)
                                        break;
                                }

                                add = bb.additional_attack(game_table);

                                if (add == -1)
                                    break;

                                ConsoleUpdate("~~~~~~~[YOU TAKE THE CARDS]~~~~~~~", 1000, true);

                                game_table.Add(bb.koloda[add]);
                                bb.koloda.RemoveAt(add);

                                counter++;
                            }

                            moves_it = 0;

                            ConsoleUpdate("~~~~~~~[END OF THE ATTACK]~~~~~~~", 2500, true);

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