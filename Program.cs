using System;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace C__Snake_Game
{
    class Snake
    {
        static Random random = new Random();

        static int areaHeight = 20;
        static int areaWidth = 30;

        static int[] X = new int[100];
        static int[] Y = new int[100];

        static int foodX;
        static int foodY;

        static int timer = 200;

        static int tails = 1;


        static ConsoleKey arrow = ConsoleKey.DownArrow;

        static void WriteBoarder() {
            Clear();

            for (int i = 1; i < (areaWidth + 2); i++) {
                SetCursorPosition(i, 1);
                Write("-");
            }
            for (int i = 1; i < (areaWidth + 2); i++) {
                SetCursorPosition(i, (areaHeight + 2));
                Write("-");
            }
            for (int i = 1; i < (areaHeight + 2); i++) {
                SetCursorPosition(1, i);
                Write("|");
            }
            for (int i = 1; i < (areaHeight + 2); i++) {
                SetCursorPosition((areaWidth + 2), i);
                Write("|");
            }
        }

        static Task Input()
        {
            return Task.Run(() =>
            {
                arrow = ReadKey(true).Key;
            });
        }

        static void WriteSnakeOrFood(int x, int y, char z)
        {
            SetCursorPosition(x, y);
            Write(z);
        }

        static void Logic()
        {
            if (X[0] == foodX && Y[0] == foodY)
            {
                tails++;
                foodX = random.Next(2, (areaWidth - 2));
                foodY = random.Next(2, (areaHeight - 2));

                if (tails >= 5) {
                    if (timer > 5)
                    {
                        timer -= 5;
                    }
                }
            }

            for (int i = tails; i > 1; i--)
            {
                X[i - 1] = X[i - 2];
                Y[i - 1] = Y[i - 2];
            }

            switch (arrow)
            {
                case ConsoleKey.UpArrow: Y[0]--; break;
                case ConsoleKey.DownArrow: Y[0]++; break;
                case ConsoleKey.RightArrow: X[0]++; break;
                case ConsoleKey.LeftArrow: X[0]--; break;
            }

            for (int i = 0; i < tails; i++) {
                WriteSnakeOrFood(X[i], Y[i], '*');
                WriteSnakeOrFood(foodX, foodY, '#');
            }

            Thread.Sleep(timer);
        }

        static bool GameOver()
        {
            if (X[0] == 1 || X[0] == areaWidth || Y[0] == 1 || Y[0] == areaHeight)
            {
                return true;
            }
            else
            {
                for (int i = 1; i < tails; i++)
                {
                    if (X[i] == X[0] && Y[i] == Y[0])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        static void Main(string[] args)
        {
            X[0] = 7;
            Y[0] = 7;
            CursorVisible = false;
            foodX = random.Next(2, (areaWidth - 2));
            foodY = random.Next(2, (areaHeight - 2));
            while (true)
            {
                WriteBoarder();
                Input();
                Logic();
                if (GameOver() == true)
                {
                    Clear();
                    WriteLine("****************************************************");
                    WriteLine("****************************************************");
                    WriteLine("****************************************************");
                    WriteLine("******************** Game Over! ********************");
                    WriteLine("****************************************************");
                    WriteLine("****************************************************");
                    WriteLine("****************************************************");
                    break;
                }
            }
        }
    }
}
