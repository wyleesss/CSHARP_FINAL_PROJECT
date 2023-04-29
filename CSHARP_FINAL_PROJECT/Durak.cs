using System;

namespace Durak
{
    class Player
    {
     /* int rating; */

        public string name { get; }
        public List<Card> koloda { get; set; } = new();
        public List<SpecialAbilities> sa { get; }
        public bool is_playing { get; set; }

        public Player(string name, List<SpecialAbilities> sa, bool is_playing)
        {
            this.name = name;
            this.sa = sa;
            this.is_playing = is_playing;
        }
    }

    class BOTIK_BATON 
    {
        public List<Card> koloda { get; set; } = new();
        public bool is_playing { get; set; }
        public char koz { get; set; }
        BOTIK_BATON(List<Card> koloda, bool isp, char koz) 
        {
            this.koloda = koloda;
            this.is_playing = isp;
            this.koz = koz;
            Card.sort(this.koloda);
        }
        bool check_cards(Card ch) 
        {
            foreach (var card in koloda) 
            {
                if (card.suit == koz) return true;
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
                }
                for (int i = 0; i < koloda.Count; i++)
                {
                    if (koloda[i].suit == koz) return i;
                }
            }
            return -1;
        }

        public int attack(List<Card> still) 
        {
            if (still.Count == 0)
            {
                Random r = new();
                return r.Next(0, koloda.Count());
            }
            else 
            {
                for (int i = 0; i < still.Count(); i++)
                {
                    for (int j = 0; j < koloda.Count(); j++)
                    {
                        if (still[i].number == koloda[j].number) return j;
                    }
                }
            }
            return -1;
        }


    }

    class Game
    {
        Player player;
        BOTIK_BATON bb;
        List<Card> koloda;
        List<Card> rebound = new();
        char trump_suit;

     /* List<Card> game_table = new();*/
        bool transferable;

        public Game(int min_card, Player player,BOTIK_BATON bb, bool transferable = false)
        {
            this.player = player;
            this.bb = bb;   
            this.transferable = transferable;

            koloda = Card.Schuffle(min_card);
            koloda = Card.Mix(koloda);
            trump_suit = koloda[koloda.Count - 1].suit;
        }

        public void give_cards()
        {
            for (int i = 0; i < 6 - player.koloda.Count; i++)
            {
                player.koloda.Add(koloda[i]);
                koloda.RemoveAt(i);
            }


            for (int i = 0; i < 6 - bb.koloda.Count; i++)
            {
                bb.koloda.Add(koloda[i]);
                koloda.RemoveAt(i);
            }
        }

     /* public void take() {  } */

     /* public void attack() {  } */

     /* public void deffend() {  } */

     /* public void transfer() {  } */

     /* public void bot_do() {  } */

        public void mixing()
        {
            koloda = Card.Mix(rebound);
            rebound.Clear();
            trump_suit = koloda[koloda.Count - 1].suit;
        }
    }
}