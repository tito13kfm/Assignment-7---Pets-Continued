using System;
using System.Collections.Generic;

namespace Assignment_7___Pets_Continued
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //declare a list of Pet objects
            var petList = new List<Pet>();

            //Let's draw something fancy
            Console.WriteLine(Statics.title);
            Console.WriteLine("Press enter to continue...");
            Console.ReadKey();

            //Clean up our mess
            Console.Clear();

            //Call the menu
            Menu.Main(petList);
        }
    }
}
