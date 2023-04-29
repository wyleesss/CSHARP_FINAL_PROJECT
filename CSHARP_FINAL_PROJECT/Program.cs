class Program
{
    static void Main()
    {
        List<Card> cards = new List<Card>();
        cards = Card.Schuffle(6);
        cards = Card.Mix(cards);
        foreach (Card card in cards) 
        {
            card.print();
            Console.Write(" ");
        }

        Card.sort(cards);
        Console.WriteLine();
        foreach (Card card in cards)
        {
            card.print();
            Console.Write(" ");
        }
    }
}