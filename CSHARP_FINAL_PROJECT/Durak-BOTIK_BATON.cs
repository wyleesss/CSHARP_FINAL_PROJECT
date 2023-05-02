namespace Durak
{
    class BOTIK_BATON
    {
        int take_count = 0;
        public List<Card> koloda { get; set; } = new();
        public bool is_playing { get; set; } = false;
        public char koz { get; set; }

        public BOTIK_BATON()
        { }

        bool check_cards(Card ch)
        {
            foreach (var card in koloda)
            {
                if (card.suit == koz && ch.suit != koz) return true;
                else if (card.suit == ch.suit && card.number > ch.number) return true;
            }
            return false;
        }

        public int defense(Card att, int game_table_count, int game_koloda_count)
        {
            if (check_cards(att))
            {
                for (int i = 0; i < koloda.Count; i++)
                {
                    if (game_table_count >= 3)
                    {
                        for (int j = 0; j < koloda.Count; j++)
                        {
                            if (koloda[j].suit == koz && att.suit != koz && koloda[j].number == att.number)
                                return j;
                        }
                    }

                    if (koloda[i].suit == att.suit && koloda[i].number > att.number) return i;
                    else if ((koloda[i].suit == koz && game_koloda_count <= 6) || (koloda[i].suit == koz && (take_count + 1) % 2 == 0)) return i;
                }
            }

            take_count++;
            return -1;
        }

        public int attack(List<Card> game_table, int game_koloda_count)
        {
            bool only_koz = true;

            foreach (Card card in koloda)
            {
                if (card.suit != koz)
                {
                    only_koz = false;
                    break;
                }
            }

            if (game_table.Count == 0)
            {
                Random r = new();

                if (!only_koz)
                {
                    int random_index;

                    do
                    {
                        random_index = r.Next(0, koloda.Count());
                    } 
                    while (koloda[random_index].suit == koz);

                    return random_index;
                }
                else
                {
                    return r.Next(0, koloda.Count());
                }
            }
            else if (game_table.Count <= 12)
            {
                if (only_koz && game_koloda_count < 6 && game_table.Count != 0)
                    return -1;

                for (int i = 0; i < game_table.Count(); i++)
                {
                    for (int j = 0; j < koloda.Count(); j++)
                    {
                        if (game_table[i].number == koloda[j].number)
                        {
                            if (only_koz)
                                return -1;

                            else if (koloda[j].suit == koz && game_koloda_count > 4)
                                return -1;

                            else
                                return j;
                        }
                    }
                }
            }
            return -1;
        }
    }
}