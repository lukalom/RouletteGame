using System;

namespace Class_Roulette
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Roulette game = new Roulette(1000);

        }
    }
    class Roulette
    {
        private double Balance;
        int[] red = { 32, 19, 21, 25, 34, 27, 36, 30, 23, 5, 16, 1, 14, 9, 18, 7, 12, 3 };
        int[] black = { 15, 4, 2, 17, 6, 13, 11, 8, 10, 24, 33, 20, 31, 22, 29, 28, 35, 26 };

        private int RandomNumber;
        private int RandomColor;
        private int? UserBet;
        public Roulette(int balance)
        {
            Balance = balance;

            run(); //game starter
        }

        public void run()
        {
            Random r = new Random();
            int redRandomNum = red[r.Next(0, 18)];
            int blackRandomNum = black[r.Next(0, 18)];
            RandomColor = r.Next(1, 3);
            RandomNumber = (RandomColor == 1) ? redRandomNum : blackRandomNum;

            while (Balance != 0)
            {
                UserBet = ValidBet();
                if (UserBet != null)
                {
                    int? userValidNumber = ValidNumber();
                    int? userValidColor = ValidColor();

                    if (userValidNumber != null && userValidColor != null)
                    {
                        Console.WriteLine($"Computer Random Number: {RandomNumber} color: {RandomColor}");
                        CheckWinner((int)userValidNumber, (int)userValidColor);
                    }
                    else
                    {
                        Console.WriteLine("Incorrect Number or Color");
                    }
                }
                else
                {
                    break;
                }

            }
        }

        private int? ValidNumber()
        {
            Console.Write("Choose number from (0, 35) range: ");
            bool isNumber = int.TryParse(Console.ReadLine(), out int number);

            if (isNumber && (number >= 0 && number < 36))
            {
                return number;
            }
            else
            {
                return null;
            }
        }

        private int? ValidColor()
        {
            Console.Write("Choose Color red = 1 black = 2 :");
            bool isColor = int.TryParse(Console.ReadLine(), out int color);

            if (isColor && (color == 1 || color == 2))
            {
                return color;
            }
            else
            {
                return null;
            }
        }

        private int? ValidBet()
        {
            Console.Write("Bet Money between 0 to 60: ");
            bool isBetMoney = int.TryParse(Console.ReadLine(), out int betMoney);

            if (isBetMoney && (betMoney > 0 && betMoney <= 60))
            {
                if (betMoney <= this.Balance)
                {
                    Console.WriteLine("Wish you luck ;)");
                    return betMoney;
                }
                else
                {
                    Console.WriteLine("you dont have enought money ;(");
                    return null;

                }
            }
            else
            {
                Console.WriteLine("betting error! bet in range (0, 60)");
                return null;
            }
        }

        private void CheckWinner(int userValidNumber, int userValidColor)
        {
            if (this.RandomNumber == userValidNumber && userValidColor == this.RandomColor)
            {
                Balance = (double)(Balance + UserBet * 2 + UserBet / 5);
                Console.WriteLine($"Your number and color is matching you won {UserBet * 2 + UserBet / 5} current Balance {Balance}");
            }
            else if (this.RandomNumber == userValidNumber)
            {
                Console.WriteLine("Numbers is matching but collor is incorrect");
            }
            else if (userValidColor == this.RandomColor)
            {
                Console.WriteLine("Collors is matching but number is incorrect");
            }
            else
            {
                Balance = (double)(Balance - UserBet);
                Console.WriteLine($"You lost :{UserBet} current Balance {Balance}");
                Console.WriteLine($"Your Number: {userValidNumber} color: {userValidColor}");
            }
        }
    }
}
