using System;
using FileRandomLib;

namespace PrjModule16
{
    internal static class Program
    {
        private static void Main()
        {
            Console.Write("Enter file name: ");
            var ff = new FileFiller($"{Console.ReadLine()}.txt");
            Console.Write("Enter lower limit for RNG: ");
            var ll = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter higher limit for RNG: ");
            var hl = Convert.ToInt32(Console.ReadLine());

            ff.Fill(ll, hl);
            Console.WriteLine("File was successfully filled with random numbers");

            Console.WriteLine("Want to find number? (Y/N)");
            var exitState = Console.ReadKey();

            switch (exitState.Key)
            {
                case ConsoleKey.Y:
                    Console.Write("\rEnter your number: ");
                    var numberForFind = Convert.ToInt32(Console.ReadLine());

                    Console.Write(ff.Find(numberForFind) ? "Number was found" : "Number wasn't found");
                    break;
            }

            Console.WriteLine("\nWant to add number? (Y/N)");
            exitState = Console.ReadKey();

            if (exitState.Key == ConsoleKey.Y)
            {
                Console.Write("\rEnter your number or left empty for random: ");
                var consoleRead = Console.ReadLine();
                if (!string.IsNullOrEmpty(consoleRead))
                {
                    var numberForAppend = Convert.ToInt32(consoleRead);
                    Console.WriteLine(ff.Append(numberForAppend)
                        ? "Number was append"
                        : "Number wasn't append due to duplicate");
                }
                else
                {
                    ff.Append();
                    Console.WriteLine("Number was append");
                }
            }

            var fileText = ff.ReadAllFile();
            var lineNumber = 0;
            Console.WriteLine("Press 'w' to read up or 's' to read bottom line");
            Console.WriteLine("Press 'ESC' to exit");
            while (true)
            {
                Console.Write("\r");
                Console.WriteLine($"{fileText[lineNumber]} - line number {lineNumber}");

                exitState = Console.ReadKey();

                switch (exitState.Key)
                {
                    case ConsoleKey.W when lineNumber > 0:
                        lineNumber--;
                        break;
                    case ConsoleKey.S when lineNumber < fileText.Length - 1:
                        lineNumber++;
                        break;
                    case ConsoleKey.Escape:
                        return;
                }
            }
        }
    }
}