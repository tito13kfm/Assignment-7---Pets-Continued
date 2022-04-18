using System;

namespace Assignment_7___Pets_Continued
{
    internal class IO
    {
        public static string Read(string prompt)
        {
            Console.Write(prompt + " ");
            return Console.ReadLine();
        }

        public static int ReadPosInt(string prompt) // Only accepts positive integers, and returns integer.
        {
            int integer = 0;
            bool valid = false;
            while (!valid)
            {
                valid = int.TryParse(Read(prompt), out integer);
                if (!valid) { Console.WriteLine("Please enter a valid positive integer"); }
            }
            return integer;
        }
        public static bool ReadYesNo(string prompt) // Accepts any string starting with y to be returned as true.
        {
            bool input = Read(prompt).ToLower().StartsWith("y");
            return input;
        }

        public static char ReadKey(string prompt)
        {
            Console.Write(prompt + "\n");
            char key = Console.ReadKey(true).KeyChar;
            return key;
        }
    }
}
