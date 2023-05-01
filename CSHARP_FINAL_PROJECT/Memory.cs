using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM
{
    class Player
    {
        public int score = 0;
        public int progress = 0;
    }
    class Memory
    {
        public Card[,] board = new Card[4, 8];
        Card[,] tempboard = new Card[4, 8];
        Player player = new Player();
        public Memory()
        {
            List<Card> tempdeck;
            tempdeck = Card.Schuffle(11, 2);
            tempdeck = Card.Mix(tempdeck);
            int k = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    this.board[j, i] = tempdeck[k];
                    k++;
                }
            }

        }
        public void go()
        {
            int firstcolumn = -6, firstrow = -6, secondcolumn = -6, secondrow = -6;
            string str = "";
            string[] arr;

            while (firstcolumn >= 4 || firstrow >= 8 || firstcolumn < 0 || firstrow < 0)
            {
                // firstcolumn = -6; firstrow = -6; secondcolumn = -6; secondrow = -6;
                Console.WriteLine("Enter the position of the first card:");
                str = Console.ReadLine();
                arr = str.Split(',', '.', ' ');
                try
                {
                    firstcolumn = int.Parse(arr[0]) - 1;
                    firstrow = int.Parse(arr[1]) - 1;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message); 
                }
                    if (firstcolumn >= 4 || firstrow >= 8 || firstcolumn < 0 || firstrow < 0)
                        Console.WriteLine("You have entered an invalid number!");
                }
                while (secondcolumn >= 5 || secondrow >= 8 || secondcolumn < 0 || secondrow < 0 || (secondcolumn==firstcolumn && firstrow==secondrow))
                {
                    Console.WriteLine("Enter the position of the second card:");
                    str = Console.ReadLine();
                    arr = str.Split(',');
                try 
                {
                    secondcolumn = int.Parse(arr[0]) - 1;
                    secondrow = int.Parse(arr[1]) - 1;
                }   
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                if (secondcolumn >= 5 || secondrow >= 8 || secondcolumn < 0 || secondrow < 0)
                        Console.WriteLine("You have entered an invalid number!");
                    if(secondcolumn == firstcolumn && firstrow == secondrow)
                    Console.WriteLine("You entered the same number as the first card!");
            }
            
            //Console.WriteLine(Convert.ToString(board[firstcolumn, firstrow].number), Convert.ToString(board[firstcolumn, firstrow].suit), Convert.ToString( board[secondcolumn, secondrow].suit), Convert.ToString(board[secondcolumn, secondrow].suit));
            if (checking(firstcolumn, firstrow, secondcolumn, secondrow))
            {
                tempboard[firstcolumn, firstrow] = board[firstcolumn, firstrow];
                tempboard[secondcolumn, secondrow] = board[firstcolumn, firstrow];
                player.score++;
                player.progress++;
            }
            else
            {
                Console.Write("First card:");
                board[firstcolumn, firstrow].print();
                Console.WriteLine();
                Console.Write("Second card:");
                board[secondcolumn, secondrow].print();
                Console.WriteLine();
                player.progress++;
                Console.ReadKey();
            }

        }
        bool checking(int fcolumn, int frow, int scolumn, int srow)
        {
            if (board[fcolumn, frow].number == board[scolumn, srow].number && board[fcolumn, frow].suit == board[scolumn, srow].suit)
                return true;
            else
                return false;
        }
        void printBoard(Card[,] list)
        {
            for (int i = 1; i <= 4; i++)
                Console.Write("\t"+Convert.ToString(i));

            //Console.WriteLine(5);
            for (int i = 0; i < 8; i++)
            {
                Console.WriteLine();
                Console.Write($"{i + 1}"+"\t");
                for (int j = 0; j < 4; j++)
                {
                    if (list[j, i] != null)
                        list[j, i].print();
                    else
                        Console.Write("##");
                    Console.Write("\t");
                }
                
            }
            Console.WriteLine();
        }
        public void game()
{

    while (player.score < 16)
    {
                Console.Clear();
                printBoard(tempboard);
        Console.WriteLine("score: " + player.score);
        Console.WriteLine("moves: " + player.progress);
         printBoard(board);
        go();

    }
            printBoard(tempboard);
            Console.WriteLine($"Congratulations! You won with the number of moves: {player.progress}") ;
  


}
    }
}