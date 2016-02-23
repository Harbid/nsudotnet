using System;

namespace Harbid.Nsudotnet.NumberGuesser
{
    class Game
    {
        private static string Exit = "q";
        private static int ExitState = -1;
        private static int NullString = -2;
        private static int WrongArgument = -3;
        private static string[] Swears = { ", ебать ты лох.", ", ну ты тупой.", ", давай пробуй еще.", ", уже можно было бы и отгадать..." };

        private string[] History;
        private string UserName;
        private int Number;
        private Random Rnd;
        private DateTime StartTime;
        
        public Game(string userName)
        {
            Rnd = new Random();
            History = new string[1000];
            UserName = userName;
            Number = Rnd.Next(0,101);
            StartTime = DateTime.Now;
        }

        public void ShowResults(int attempts)
        {
            Console.WriteLine("Попыток: {0}", attempts + 1);
            Console.WriteLine("История игры:");
            for (int i = 0; i <= attempts; i++)
            {
                Console.WriteLine(History[i]);
            }
            TimeSpan played = DateTime.Now - StartTime;

            Console.WriteLine("Время, затраченное на угадывание (в минутах): {0}", played.Minutes);
            return;
        }

        public void Start()
        {
            Console.WriteLine("> {0}, погнали!", UserName);
            Console.WriteLine("> Какое число я загадал?");
            int guessedNumber = 101;
            int wrongGuesses = 0;
            int currentTry = 0;
            
            while (guessedNumber != ExitState)
            {
                guessedNumber = ReadNumber();

                if (guessedNumber == ExitState)
                {
                    Console.WriteLine("> Извините, затыкаюсь.");
                    continue;
                }

                if (guessedNumber == NullString) { continue; }

                if (guessedNumber == WrongArgument)
                {
                    Console.WriteLine("> {0}, нужно ввести число от 0 до 100.", UserName);
                    continue;
                }

                if (guessedNumber == Number)
                {
                    Console.WriteLine("> {0} красаучег, угадал!", UserName);                     
                    History[currentTry] = String.Format("{0}, угадал", guessedNumber);
                    ShowResults(currentTry);
                    guessedNumber = ExitState; 
                }
                else
                {
                    wrongGuesses++;
                    if (wrongGuesses == 4)
                    {
                        wrongGuesses = 0;
                        Console.WriteLine("> {0}{1}", UserName, Swears[Rnd.Next(0, Swears.Length)]);
                    }
                    if (guessedNumber < Number)
                    {
                        Console.WriteLine("> Больше.");
                        History[currentTry] = String.Format("{0}, больше", guessedNumber);
                    }
                    else
                    {
                        Console.WriteLine("> Меньше.");
                        History[currentTry] = String.Format("{0}, меньше", guessedNumber);                       
                    }
                    currentTry++;
                }
            }
            return;
        }

        private int ReadNumber()
        {
            int number;
            string input = Console.ReadLine();

            if (String.IsNullOrEmpty(input))
            {
                //Console.WriteLine("NULL string.");
                return NullString;
            }
            if (input == Exit)
            {
                //Console.WriteLine("Exit command.");
                return ExitState;
            }
            try
            {
                number = int.Parse(input);
                if (number < 0 || number > 100)
                {
                    //Console.WriteLine("Number should be in {0...100}.");
                    return WrongArgument;
                }/*
                else
                {
                    Console.WriteLine("Correct number value.");
                }*/
            }
            catch (FormatException)
            {
                //Console.WriteLine("Incorrect string.");
                return WrongArgument;                 
            }
            return number;
        }
    }
}
