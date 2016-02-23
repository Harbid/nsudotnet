using System;

namespace Harbid.Nsudotnet.NumberGuesser
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("> Как тебя зовут?");
            string userName = Console.ReadLine();
            Game game = new Game(userName);
            game.Start();
            Console.ReadKey();
            return;
        }
    }
}
