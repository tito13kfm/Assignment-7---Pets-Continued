using System;

namespace Assignment_7___Pets_Continued
{
    internal class Pet
    {
        public string name, breed;
        public int age;
        public bool spayed;
        public static int totalNumberOfPets = 0, sumOfAllPetAges = 0;
        public static bool allFixed = true; // need to declare this true by default so any single false will make it false

        public static void PrintPetStats()
        {
            Console.WriteLine("You have {0} pets", totalNumberOfPets);
            Console.WriteLine("Their ages add up to {0}", sumOfAllPetAges);
            double sum = (double)sumOfAllPetAges / (double)totalNumberOfPets; // figure out average age
            Console.WriteLine("Which means their average age is approximately {0}", sum.ToString("#.##")); // format for 2 decimal points
            Console.WriteLine((allFixed) ? "Thank you for helping to control the pet population" : "Help control the pet population, have your pets spayed or neutered"); //RIP Bob
        }
    }
}
