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
                if (game_koloda_count < 6) 
                {
                    //Бити при кінці козирем таку саму карту як вона за числом
                    for (int i = 0; i < koloda.Count; i++)
                    {
                        if (att.number == koloda[i].number && koloda[i].suit == koz) return i;
                    }
                    //Якщо не знайшли то шукає просто чим можна відбити
                    for (int i = 0; i < koloda.Count; i++)
                    {
                        if (att.suit == koloda[i].suit && koloda[i].number > att.number) return i;
                    }

                    for (int i = 0; i < koloda.Count; i++)
                    {
                        if (koloda[i].suit == koz && koloda[i].number > att.number) return i;
                    }
                }

                //Якщо карта козирь і ми не брали два рази і якщо в оклоді достатнь карт ми беремо козирь
                if (att.suit == koz && take_count % 2 != 0 && game_koloda_count > 6) return -1;
                else
                {
                    //Шукаємо просто чим відбити
                    //Для оптимізації застосовуємо розгалуження
                    if (att.suit == koz)
                    {
                        //Шукаємо козирь зразу ж
                        for (int i = 0; i < koloda.Count; i++)
                        {
                            if (att.suit == koloda[i].suit && koloda[i].number > att.number) return i;
                        }
                    }
                    else
                    {
                        //Шукаємо просту карту
                        for (int i = 0; i < koloda.Count; i++)
                        {
                            if (att.suit == koloda[i].suit && koloda[i].number > att.number) return i;
                        }
                        //Якщо побити можемо тільки козерем то ми забираємо якщо ще багато карт у колоді
                        if (game_koloda_count >= 12 || game_table_count < 4) return -1;
                        //Якщо не знаходимо то шукаємо козирь
                        for (int i = 0; i < koloda.Count; i++)
                        {
                            if (koloda[i].suit == koz && koloda[i].number > att.number) return i;
                        }
                    }
                }
            }

            take_count++;
            return -1;
        }

        public int additional_attack(List<Card> game_table)
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

            if (only_koz)
                return -1;

            for (int i = 0; i < game_table.Count(); i++)
            {
                for (int j = 0; j < koloda.Count(); j++)
                {
                    if (game_table[i].number == koloda[j].number
                        && koloda[j].suit != koz)
                        return j;
                }
            }

            return -1;
        }
        int find_min_card_index()
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

            int min_number = int.MaxValue;
            int index = 0;

            for (int i = 0; i < koloda.Count; i++)
            {
                if (!only_koz)
                {
                    if (koloda[i].number < min_number && koloda[i].suit != koz)
                    {
                        min_number = koloda[i].number;
                        index = i;
                    }
                }
                else
                {
                    if (koloda[i].number < min_number)
                    {
                        min_number = koloda[i].number;
                        index = i;
                    }
                }
            }

            return index;
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
                return find_min_card_index();
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