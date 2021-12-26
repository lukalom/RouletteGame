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
        private readonly int[] red = { 32, 19, 21, 25, 34, 27, 36, 30, 23, 5, 16, 1, 14, 9, 18, 7, 12, 3 };

        private int RandomNumber;
        private int? UserBet;
        public Roulette(int balance)
        {
            Balance = balance;

        }
        public void Run()
        {
            Random r = new Random();

            while (Balance != 0)
            {
                RandomNumber = r.Next(0, 36);

                var selectGameMode = SelectGameMode();
                this.UserBet = SelectBet();

                if (selectGameMode != null)
                {
                    if (selectGameMode == 1)
                    {
                        int? userSelectNumber = SelectNumber();
                        if (userSelectNumber != null)
                        {
                            CheckNumberWinner((int)userSelectNumber);
                            if (TryAgain() == true)
                            {
                                continue;
                            }
                            else { break; }
                        }
                        else
                        {
                            Console.WriteLine("Enter correct number");
                            continue;
                        }

                    }
                    else if (selectGameMode == 2)
                    {
                        int? userSelectColor = SelectColor();
                        if (userSelectColor != null)
                        {
                            CheckColorWinner((int)userSelectColor);
                            if (TryAgain() == true)
                            {
                                continue;
                            }
                            else { break; }
                        }
                        else
                        {
                            Console.WriteLine("Enter correct Color");
                            continue;
                        }

                    }
                    else
                    {
                        continue;
                    }
                }

            }
        }

        private int? SelectGameMode()
        {
            Console.Write("(1) only numbers, (2) only colors: ");
            bool isGameMode = int.TryParse(Console.ReadLine(), out int gameMode);

            if (isGameMode && (gameMode == 1 || gameMode == 2))
            {
                return gameMode == 1 ? 1 : 2;
            }
            else
            {
                Console.WriteLine("Enter correct game mode ");
                return null;
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

        private void CheckColorWinner(int userSelectColor)
        {
            if (red.Contains(RandomNumber) && userSelectColor == 2)
            {
                Balance += (double)(UserBet + (UserBet / 5));
                Console.WriteLine($"you won: {UserBet + (UserBet / 5)}: current balance {Balance}");

            }
            else if (!red.Contains(RandomNumber) && userSelectColor == 1)
            {
                Balance += (double)(UserBet + (UserBet / 5));
                Console.WriteLine($"you won: {UserBet + (UserBet / 5)}: current balance {Balance}");

            }
            else
            {
                Balance -= (double)UserBet;
                Console.WriteLine($"you lost: {UserBet} current balance {Balance}");

            };
        }

        private void CheckNumberWinner(int userSelectNumber)
        {
            if (RandomNumber == userSelectNumber)
            {
                if (userSelectNumber == 0)
                {
                    Balance += (double)(36 * UserBet);
                    Console.WriteLine($"its Zero you Won 36X current balance: {Balance}");
                }
                else
                {
                    Balance += (double)(UserBet * 2);
                    Console.WriteLine($"you won: {UserBet * 2}: current balance {Balance}");
                }
            }

            else
            {
                Balance -= (double)(UserBet);
                Console.WriteLine($"you lost: {UserBet} current balance {Balance} computer number {RandomNumber}");
            };

        }

        private bool TryAgain()
        {
            Console.Write("try again (y/n): ");
            var move = Console.ReadLine();

            if (!string.IsNullOrEmpty(move) && move.ToLower() == "y")
            {
                return true;
            }
            else
            {
                Console.WriteLine("your balance {0}", Balance);
                return false;
            }
        }
    }
}
