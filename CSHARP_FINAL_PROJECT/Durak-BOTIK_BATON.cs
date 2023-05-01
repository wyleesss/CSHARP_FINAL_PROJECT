namespace Durak
{
    class BOTIK_BATON
    {
        public List<Card> koloda { get; set; } = new();
        public bool is_playing { get; set; } = false;
        public char koz { get; set; }

        public BOTIK_BATON()
        {
            koloda = Card.SortNum(koloda);
        }

        bool check_cards(Card ch)
        {
            foreach (var card in koloda)
            {
                if (card.suit == koz && ch.suit != koz) return true;
                else if (card.suit == ch.suit && card.number > ch.number) return true;
            }
            return false;
        }

        public int defense(Card att)
        {
            if (check_cards(att))
            {
                for (int i = 0; i < koloda.Count; i++)
                {
                    if (koloda[i].suit == att.suit && koloda[i].number > att.number) return i;
                    else if (koloda[i].suit == koz) return i;
                }
            }

            return -1;
        }

        public int attack(List<Card> game_table)
        {
            if (game_table.Count == 0)
            {
                Random r = new();
                bool only_koz = true;

                foreach (Card card in koloda) 
                {
                    if (card.suit != koz) 
                    {
                        only_koz = false;
                        break;
                    }
                }

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
                for (int i = 0; i < game_table.Count(); i++)
                {
                    for (int j = 0; j < koloda.Count(); j++)
                    {
                        if (game_table[i].number == koloda[j].number) return j;
                    }
                }
            }
            return -1;
        }
    }
}