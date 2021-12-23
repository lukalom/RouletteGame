using System;
using System.Linq;

namespace Class.Roulette
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Roulette game = new Roulette(1000);
            game.Run();
        }
    }
    class Roulette
    {
        private double Balance;
        int[] red = { 32, 19, 21, 25, 34, 27, 36, 30, 23, 5, 16, 1, 14, 9, 18, 7, 12, 3 };

        private int RandomNumber;
        private int? UserBet;
        public Roulette(int balance)
        {
            Balance = balance;

        }

        public void Run()
        {
            Random r = new Random();
            RandomNumber = r.Next(0, 36);

            while (Balance != 0)
            {
                UserBet = SelectBet();
                if (UserBet != null)
                {
                    int? userSelectNumber = SelectNumber();

                    if (userSelectNumber == 0)
                    {

                        Balance += (double)(36 * UserBet);
                        Console.WriteLine($"its Zero you Won 36X current balance: {Balance}");

                    }
                    else
                    {
                        int? userSelectColor = SelectColor();

                        if (userSelectNumber != null && userSelectColor != null)
                        {

                            CheckWinner((int)userSelectNumber, (int)userSelectColor);
                        }
                        else
                        {
                            Console.WriteLine("Incorrect Number or Color");

                        }
                    }

                }
                else
                {
                    continue;
                }

            }
        }

        private int? SelectNumber()
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

        private int? SelectColor()
        {
            Console.Write("Choose Color black = 1 red = 2 :");
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

        private int? SelectBet()
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

        private void CheckWinner(int userSelectNumber, int userSelectColor)
        {
            if (RandomNumber == userSelectNumber && userSelectColor == 1 && !this.red.Contains(userSelectNumber) && !red.Contains(RandomNumber))
            {
                Balance += (double)(2 * UserBet + UserBet / 5);
                Console.WriteLine($"you Won {2 * UserBet + UserBet / 5} current balance: {Balance}");
                Console.WriteLine($"you color {userSelectColor} number {userSelectNumber}");
            }
            else if (RandomNumber == userSelectNumber && userSelectColor == 2 && red.Contains(userSelectNumber) && red.Contains(RandomNumber))
            {
                Balance += (double)(2 * UserBet + UserBet / 5);
                Console.WriteLine($"you Won {2 * UserBet + UserBet / 5} current balance: {Balance}");
                Console.WriteLine($"you color {userSelectColor} number {userSelectNumber}");
            }
            else if (RandomNumber == userSelectNumber)
            {
                Balance += (double)(2 * UserBet);
                Console.WriteLine($"you Won {2 * UserBet} current balance: {Balance}");
            }
            else if (userSelectColor == 1 && !red.Contains(RandomNumber))
            {
                Balance += (double)(UserBet / 5);
                Console.WriteLine($"you Won {UserBet / 5} current balance: {Balance}");
            }
            else if (userSelectColor == 2 && red.Contains(RandomNumber))
            {
                Balance += (double)(UserBet / 5);
                Console.WriteLine($"you Won {UserBet / 5} current balance: {Balance}");
            }
            else
            {
                Balance -= (double)(UserBet);
                Console.WriteLine($"You lost computer picked number: {RandomNumber} current balance: {Balance}");

            }
        }
    }
}
