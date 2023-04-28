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

    class Game
    {
        Player player;
     /* Bot bot */

        List<Card> koloda;
        List<Card> rebound = new();
        char trump_suit;

     /* List<Card> game_table = new();*/
        bool transferable;

        public Game(int min_card, Player player, bool transferable = false)
        {
            this.player = player;
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