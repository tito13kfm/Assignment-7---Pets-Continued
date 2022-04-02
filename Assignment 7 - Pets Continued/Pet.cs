using System;
using System.Collections.Generic;

namespace Assignment_7___Pets_Continued
{
    internal class Pet
    {
        public string name, breed;
        public int age;
        public bool spayed;
        public static bool done;
        public static int totalNumberOfPets = 0, sumOfAllPetAges = 0;
        public static bool allFixed = true; // need to declare this true by default so any single false will make it false

        /// <summary>
        /// Prints out a summary of the Pets.  The total number owned, their age total, and the average age.
        /// Also checks if all Pets are fixed and displays message appropriately.
        /// </summary>
        public static void PrintPetSummary()
        {
            Console.WriteLine("You have {0} pets", totalNumberOfPets);
            Console.WriteLine("Their ages add up to {0}", sumOfAllPetAges);
            double sum = (double)sumOfAllPetAges / (double)totalNumberOfPets; // figure out average age
            Console.WriteLine("Which means their average age is approximately {0}", sum.ToString("#.##")); // format for 2 decimal points
            Console.WriteLine((allFixed) ? "Thank you for helping to control the pet population" : "Help control the pet population, have your pets spayed or neutered"); //RIP Bob
            Console.ReadKey();
        }


        /// <summary>
        /// Add 1 to current age of Pet and display birthday celebration message
        /// </summary>
        public void HappyBirthday()
        {
            Console.Clear();
            Console.WriteLine("Happy Birthday {0}!!", name);
            age++;
            Console.WriteLine("{0} is now {1} years old!", name, age);
            Console.ReadKey();
        }

        /// <summary>
        /// Add or subtract from totalNumberOfPets and update sumOfAllPetAges
        /// </summary>
        /// <param name="addto">true if adding pet</param>
        public void AddRemove(bool addto)
        {
            totalNumberOfPets = (addto) ? totalNumberOfPets + 1 : totalNumberOfPets - 1;
            sumOfAllPetAges = (addto) ? sumOfAllPetAges += age : sumOfAllPetAges -= age;
        }

        public static void AddPets(List<Pet> petList)
        {
            done = false;
            while (!done)
            {
                Pet newPet = new Pet();
                newPet.name = IO.Read("What is the name of pet #" + (petList.Count + 1) + ":");
                newPet.age = IO.ReadPosInt("How old is " + newPet.name + ":");
                newPet.breed = IO.Read("What breed is " + newPet.name + ":");
                newPet.spayed = IO.ReadYesNo("Is " + newPet.name + " fixed? (Yes/No):");
                newPet.AddRemove(true);
                Pet.allFixed = Pet.allFixed & newPet.spayed;
                petList.Add(newPet);
                done = IO.ReadYesNo("Do you want to add another pet?") ? false : true;
            }
        }
    }
}
