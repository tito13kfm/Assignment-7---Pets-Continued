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
        /// <param name="addto">true if adding, false if removing</param>
        public void AddRemove(bool addto)
        {
            totalNumberOfPets = (addto) ? totalNumberOfPets + 1 : totalNumberOfPets - 1;
            sumOfAllPetAges = (addto) ? sumOfAllPetAges += age : sumOfAllPetAges -= age;
        }

        /// <summary>
        /// Gather details of Pets and add their references to the List
        /// </summary>
        /// <param name="petList">List of Pets to add Pets to</param>
        public static void AddPets(List<Pet> petList)
        {
            done = false;
            while (!done)
            {
                Pet newPet = new Pet();
                Console.Clear();
                newPet.name = IO.Read("What is the name of pet #" + (petList.Count + 1) + ":");
                newPet.age = IO.ReadPosInt("How old is " + newPet.name + ":");
                newPet.breed = IO.Read("What breed is " + newPet.name + ":");
                newPet.spayed = IO.ReadYesNo("Is " + newPet.name + " fixed? (Yes/No):");
                newPet.AddRemove(true);
                petList.Add(newPet);
                done = IO.ReadYesNo("Do you want to add another pet?") ? false : true;
            }
        }

        /// <summary>
        /// Checks if every Pet in a List of Pets is fixed.
        /// </summary>
        /// <param name="petList">List of Pets to evaluate if they are all fixed</param>
        public static void UpdateFixed(List<Pet> petList)
        {
            allFixed = true;
            foreach (Pet pet in petList)
            {
                allFixed = allFixed & pet.spayed;
            }
        }

        /// <summary>
        /// Print out a list of formatted and Boxified text to the Console of Pet names, ages, breed, and if they
        /// are fixed.
        /// </summary>
        /// <param name="petList">List of Pets to print details of</param>
        public static void PrintPetDetails(List<Pet> petList)
        {
            Random random = new Random();
            Console.Clear();
            List<string> petStats = new List<string>();
            foreach (Pet p in petList)
            {
                string s = String.Format("You have a {0} named {1} who is {2} years old and is {3}fixed.", p.breed, p.name, p.age, p.spayed ? "" : "NOT ");
                petStats.Add(s);
            }
            int length = Boxify.FindLongest(petStats);
            Console.WriteLine(Boxify.BoxMe(petStats, length, 'L', random.Next(1, 9)));
            Console.ReadKey();
        }


        /// <summary>
        /// Print out a list of formatted and Boxified text to the Console of your youngest and oldest pets
        /// </summary>
        /// <param name="petList">List of Pets to print Youngest and Oldest</param>
        public static void PrintPetAge(List<Pet> petList)
        {
            int youngest = petList[0].age;
            int oldest = petList[0].age;
            string stringYoung = petList[0].name, stringOld = petList[0].name;
            int youngCount = 1, oldCount = 1; // let's keep a count so we can make the english language make sense below


            //Loop to check age against assumed youngest and oldest pets.  Build strings based on age of pet being evaluated
            for (int i = 1; i < petList.Count; i++)
            {
                switch (petList[i].age)
                {
                    case int n when n < youngest:
                        stringYoung = petList[i].name;
                        youngest = petList[i].age;
                        youngCount = 1;
                        break;
                    case int n when n == youngest:
                        stringYoung = stringYoung + " & " + petList[i].name;
                        youngCount++;
                        break;
                    case int n when n > oldest:
                        stringOld = petList[i].name;
                        oldest = petList[i].age;
                        oldCount = 1;
                        break;
                    case int n when n == oldest:
                        stringOld = stringOld + " & " + petList[i].name;
                        oldCount++;
                        break;
                    default:
                        break;
                }
            }

            //Print the output
            Console.Clear();
            List<string> petAge = new List<string>();
            petAge.Add((youngCount > 1) ? stringYoung + " are your youngest pets" : stringYoung + " is your youngest pet");
            petAge.Add((oldCount > 1) ? stringOld + " are your oldest pets" : stringOld + " is your oldest pet");

            int length = Boxify.FindLongest(petAge);
            Console.WriteLine(Boxify.BoxMe(petAge, length, 'C', 1));
            Console.ReadKey();
        }
    }
}
