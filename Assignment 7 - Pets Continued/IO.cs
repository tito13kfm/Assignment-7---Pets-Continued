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
            while (!valid | integer <= 0)
            {
                valid = int.TryParse(Read(prompt), out integer);
                if (!valid | integer == 0) { Console.WriteLine("Please enter a valid positive integer"); }
            }
            return integer;
        }
        public static bool ReadYesNo(string prompt) // Accepts any string starting with y to be returned as true.
        {
            bool input = (Read(prompt)).ToLower().StartsWith("y");
            return input;
        }
    }
}
