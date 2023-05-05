//автори - Тетяна

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
        User user;
        public Memory(User user)
        {
            this.user = user;
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
            string text = "";
            int width = Console.WindowWidth;
            int left = 0;

            while (firstcolumn >= 4 || firstrow >= 8 || firstcolumn < 0 || firstrow < 0)
            {
                // firstcolumn = -6; firstrow = -6; secondcolumn = -6; secondrow = -6;
                text = "Enter the position of the first card:";
                left = (width / 2) - (text.Length / 2);
                Console.SetCursorPosition(left, Console.CursorTop);
                Console.WriteLine(text);
                left = (width / 2) - (4 / 2);
                Console.SetCursorPosition(left, Console.CursorTop);
                Console.Write("");
                str = new string(Console.ReadLine());
                //UserInterface.processed_input(ref str);
                arr = str.Split(',', '.', ' ');
                try
                {
                    firstcolumn = int.Parse(arr[0]) - 1;
                    firstrow = int.Parse(arr[1]) - 1;
                }
                catch (Exception ex)
                {
                    text = ex.Message;
                    left = (width / 2) - (text.Length / 2);
                    Console.SetCursorPosition(left, Console.CursorTop);
                    Console.WriteLine(text);
                    //Console.WriteLine(ex.Message);
                }
                if (firstcolumn >= 4 || firstrow >= 8 || firstcolumn < 0 || firstrow < 0)
                {
                    text = "You have entered an invalid number!";
                    left = (width / 2) - (text.Length / 2);
                    Console.SetCursorPosition(left, Console.CursorTop);
                    Console.WriteLine(text);
                }
            }
            while (secondcolumn >= 4 || secondrow >= 8 || secondcolumn < 0 || secondrow < 0 || (secondcolumn == firstcolumn && firstrow == secondrow))
            {
                text = "Enter the position of the second card:";
                left = (width / 2) - (text.Length / 2);
                Console.SetCursorPosition(left, Console.CursorTop);
                Console.WriteLine(text);
                left = (width / 2) - (4 / 2);
                Console.SetCursorPosition(left, Console.CursorTop);
                Console.Write("");
                str = new string(Console.ReadLine());
                arr = str.Split(',');
                try
                {
                    secondcolumn = int.Parse(arr[0]) - 1;
                    secondrow = int.Parse(arr[1]) - 1;
                }
                catch (Exception ex)
                {
                    text = ex.Message;
                    left = (width / 2) - (text.Length / 2);
                    Console.SetCursorPosition(left, Console.CursorTop);
                    Console.WriteLine(text);
                }
                if (secondcolumn >= 4 || secondrow >= 8 || secondcolumn < 0 || secondrow < 0)
                {
                    text = "You have entered an invalid number!";
                    left = (width / 2) - (text.Length / 2);
                    Console.SetCursorPosition(left, Console.CursorTop);
                    Console.WriteLine(text);
                }
                if (secondcolumn == firstcolumn && firstrow == secondrow)
                {
                    text = "You entered the same number as the first card!";
                    left = (width / 2) - (text.Length / 2);
                    Console.SetCursorPosition(left, Console.CursorTop);
                    Console.WriteLine(text);

                }
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
                text = "First card:" + board[firstcolumn, firstrow];
                //board[firstcolumn, firstrow].print();
                left = (width / 2) - (text.Length / 2);
                Console.SetCursorPosition(left, Console.CursorTop);
                Console.Write(text);
                Console.WriteLine();
                text = "Second card:" + board[secondcolumn, secondrow];
                left = (width / 2) - (text.Length / 2);
                Console.SetCursorPosition(left, Console.CursorTop);
                Console.Write(text);
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
            int width = Console.WindowWidth;
            string text = "";
            //int left = (width / 2) - (5 / 2);
            for (int i = 1; i <= 4; i++)
            {
                text = text + "     " + Convert.ToString(i);
            }

            //Console.WriteLine(5);
            string[] stringOfText = { "", "", "", "", "", "", "", "" };
            for (int i = 0; i < 8; i++)
            {
                //left = (width / 2) - (5 / 2);
                stringOfText[i] = stringOfText[i] + Convert.ToString(i + 1) + "    ";
                //Console.WriteLine();
                //Console.SetCursorPosition(left, Console.CursorTop);
                // Console.Write($"{i + 1}"+"\t");
                for (int j = 0; j < 4; j++)
                {
                    //left = (width / 2) - (4 / 2);
                    if (list[j, i] != null)
                    {
                        stringOfText[i] = stringOfText[i] + Convert.ToString(list[j, i]);
                    }

                    else
                    {
                        stringOfText[i] = stringOfText[i] + "##";
                        //Console.Write("##");
                    }

                    if (j + 1 < 4)
                    { stringOfText[i] = stringOfText[i] + "    "; }

                }
            }
            int left = (width / 2) - (stringOfText[2].Length / 2);
            //Console.WriteLine(text);
            Console.SetCursorPosition(left, Console.CursorTop);
            Console.Write(text);
            for (int i = 0; i < stringOfText.Length; i++)
            {
                Console.WriteLine();
                left = (width / 2) - (stringOfText[i].Length / 2);
                Console.SetCursorPosition(left, Console.CursorTop);
                Console.Write(stringOfText[i]);
            }
            Console.WriteLine();

        }

        public void game()
        {
            string text = "";
            int width = Console.WindowWidth;
            int left = 0;
            while (player.score < 16)
            {
                Console.Clear();
                printBoard(tempboard);
                text = "score: " + player.score;
                left = (width / 2) - (text.Length / 2);
                Console.SetCursorPosition(left, Console.CursorTop);
                Console.WriteLine(text);
                text = "moves: " + player.progress;
                left = (width / 2) - (text.Length / 2);
                Console.SetCursorPosition(left, Console.CursorTop);
                Console.WriteLine(text);
                //printBoard(board);
                go();

            }
            Console.Clear();
            printBoard(tempboard);
            text = "Congratulations! You won with the number of moves: " + player.progress;
            left = (width / 2) - (text.Length / 2);
            Console.SetCursorPosition(left, Console.CursorTop);
            Console.WriteLine(text);
            UserInterface.set_and_print("(bj :: +1500)");
            user.black_jack_b += 1500;
            Console.ReadKey();
        }
    }
}