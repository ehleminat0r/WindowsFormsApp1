using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        private static Random rnd = new Random();
        static List<int> numbers = new List<int>();

        static void Main(string[] args)
        {
            int input=-1;
            while (input <= 0)
            {
                try
                {
                    Console.Write("Bitte Zahl > 0 eingeben: ");
                    input = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Keine gueltige Zahl");
                }
            }

            for (int i = 0; i < input; i++)
            {
                numbers.Add(rnd.Next(100));
                Console.WriteLine("{0:D" + Math.Floor(Math.Log10(input) + 1) + "}. Nummer {1}", i + 1, numbers[i]);
            }

            // quer
            for (int i = 0; i < input; i++)
            {
                for (int j = 0; j < numbers[i]; j++)
                {
                    Console.Write("#");
                }

                Console.WriteLine();
            }

            Console.WriteLine();

            //hoch
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < input; j++)
                {
                    if (numbers[j] > 9-i)
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }

                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}
