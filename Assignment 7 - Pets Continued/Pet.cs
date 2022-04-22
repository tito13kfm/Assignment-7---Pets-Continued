using System;
using System.Collections.Generic;
using System.Linq;

namespace Assignment_7___Pets_Continued
{
    [Serializable()]
    internal class Pet
    {
        //Variable declarations
        public string name { get; private set; }
        public string breed { get; private set; }
        public int age { get; private set; }
        public bool spayed { get; private set; }
        
        public static int totalNumberOfPets = 0, sumOfAllPetAges = 0;
        public static bool allFixed;

        //Constructor
        /// <summary>
        /// Constructor to create a new Pet object
        /// </summary>
        /// <param name="name">Name of pet as string</param>
        /// <param name="breed">Breed of pet as string</param>
        /// <param name="age">Age of pet as int</param>
        /// <param name="spayed">Bool if pet is spayed</param>
        public Pet(string name, string breed, int age, bool spayed)
        {
            this.name = name;
            this.breed = breed;
            this.age = age;
            this.spayed = spayed;
            totalNumberOfPets++;
            sumOfAllPetAges += age;
            allFixed = allFixed & spayed;
        }


        /// <summary>
        /// Gather details of Pets and add their references to the List
        /// </summary>
        /// <param name="petList">List of Pets to add Pets to</param>
        public static void AddPets(List<Pet> petList)
        {
            bool done = false;
            while (!done)
            {
                Console.Clear();

                //Collect info on the new pet
                string name = IO.Read("What is the name of pet #" + (petList.Count + 1) + ":");
                int age = IO.ReadPosInt("How old is " + name + ":");
                string breed = IO.Read("What breed is " + name + ":");
                bool spayed = IO.ReadYesNo("Is " + name + " fixed? (Yes/No):");

                //Call set method to set the member variables
                Pet newPet = new Pet(name, breed, age, spayed);

                //Add new pet to list
                petList.Add(newPet);

                done = !IO.ReadYesNo("Do you want to add another pet?");
            }
        }

        /// <summary>
        /// Updates existing pet object member variables when called.
        /// </summary>
        public void EditPet()
        {
            bool editdone = false;

            //While loop to allow editing multiple member variables without having to call method again
            while (!editdone)
            {
                Console.Clear();
                Console.WriteLine("Editing {0}", this.name);
                Console.WriteLine("1. Change Name");
                Console.WriteLine("2. Change Age");
                Console.WriteLine("3. Change Breed");
                Console.WriteLine("4. Change Fixed status");

                Console.WriteLine();
                char selection = IO.ReadKey("Make a selection or q to quit:\n");

                //Edit member variable selected by user
                switch (selection)
                {
                    case '1':
                        //Next 3 Console.WriteLines show off 3 different ways to do the same thing
                        Console.WriteLine($"Current Name is {this.name}");
                        this.name = IO.Read("Enter new name:");
                        break;
                    case '2':
                        Console.WriteLine("Current Age is {0}", this.age);
                        this.age = IO.ReadPosInt("Enter new age:");
                        break;
                    case '3':
                        Console.WriteLine("Current Breed is " + this.breed);
                        this.breed = IO.Read("Enter new breed:");
                        break;
                    case '4':
                        string s = this.spayed ? "Unfixing " + this.name + " somehow..." : "Fixing " + this.name + ".";
                        this.spayed = this.spayed ? false : true;
                        Console.WriteLine(s);
                        Console.ReadKey();
                        break;
                    case 'q':
                        editdone = true;
                        break;
                    default:
                        Console.WriteLine("Please make a valid selection");
                        Console.WriteLine("Press Enter to continue");
                        Console.ReadKey();
                        break;
                }
            }

        }

        /// <summary>
        /// Update totalNumberofPets and sumOfAllPetAges static variables
        /// </summary>
        /// <param name="petList">List of pets to work with</param>
        public static void UpdateAgeStatics(List<Pet> petList)
        {
            totalNumberOfPets = petList.Count;

            //Set back to 0, then add up all the individual ages and save it to sumOfAllPetAges
            sumOfAllPetAges = 0;
            foreach (Pet p in petList)
            {
                sumOfAllPetAges += p.age;
            }

        }


        /// <summary>
        /// Checks if every Pet in a List of Pets is fixed.
        /// </summary>
        /// <param name="petList">List of Pets to evaluate if they are all fixed</param>
        public static void UpdateFixed(List<Pet> petList)
        {
            //assume all pets are fixed, then loop through all pets and prove so or otherwise
            allFixed = true;
            foreach (Pet pet in petList)
            {
                allFixed = allFixed & pet.spayed;
            }
        }

        /// <summary>
        /// Returns the users choice of which Pet they are selecting
        /// </summary>
        /// <param name="petList">List of Pets to work with</param>
        /// <param name="prompt">Question to ask the user</param>
        /// <returns></returns>
        public static int SelectPet(List<Pet> petList, string prompt)
        {
            PrintPetDetails(petList);
            int choice = IO.ReadPosInt(prompt) - 1;
            if (choice >= petList.Count)
            {
                return 0;
            }
            return choice;
        }
        /// <summary>
        /// Print out a list of formatted and Boxified text to the Console of Pet names, ages, breed, and if they
        /// are fixed.
        /// </summary>
        /// <param name="petList">List of Pets to print details of</param>
        public static void PrintPetDetails(List<Pet> petList)
        {
            Console.Clear();

            //Create a new list of strings to feed to boxify
            var petStats = new List<string>();

            //Create the header separator line
            var line = new string('=', 54);

            //add the strings to the list
            petStats.Add(String.Format("{0, -2} | {1,-15} | {2,-5} | {3,-10} | {4,-10}", "#", "Name", "Age", "Breed", "Fixed?"));
            petStats.Add(line);
            petStats.Add("");
            int i = 0;
            foreach (Pet p in petList)
            {
                i++;
                var s = String.Format("{0, -2} | {1,-15} | {2,-5} | {3,-10} | {4,-10}", i, p.name, p.age, p.breed, p.spayed ? "Yes" : "No ");
                petStats.Add(s);
            }

            //Write the boxified result to the screen
            Console.WriteLine(Boxify.BoxMe(petStats, 52, 'L', 1));
        }

        /// <summary>
        /// Prints out a summary of the Pets.  The total number owned, their age total, and the average age.
        /// Also checks if all Pets are fixed and displays message appropriately.
        /// </summary>
        public static void PrintPetSummary()
        {
            //Create a new list of strings to feed to boxify
            var petSummary = new List<string>();

            Console.Clear();

            //Build the strings and add them to the list
            petSummary.Add(String.Format("You have {0} pets", Pet.totalNumberOfPets));
            petSummary.Add(String.Format("Their ages add up to {0}", Pet.sumOfAllPetAges));
            double sum = (double)Pet.sumOfAllPetAges / (double)Pet.totalNumberOfPets; // figure out average age
            petSummary.Add(String.Format("Which means their average age is approximately {0}", sum.ToString("#.##"))); // format for 2 decimal points
            petSummary.Add(String.Format((Pet.allFixed) ? "Thank you for helping to control the pet population" : "Help control the pet population, have your pets spayed or neutered")); //RIP Bob

            //Determine longest string in list for sizing of box
            int length = Boxify.FindLongest(petSummary);

            //Write the box to the screen
            Console.WriteLine(Boxify.BoxMe(petSummary, length, 'C', 2));
            Console.ReadKey();
        }

        /// <summary>
        /// Accept a list of Pets, sort it by age, and return a sorted list
        /// </summary>
        /// <param name="petList">List of Pets to sort</param>
        /// <returns>Sorted List of Pets</returns>
        public static List<Pet> SortListByAge(List<Pet> petList)
        {
            return petList.OrderBy(x => x.age).ToList();
        }

        /// <summary>
        /// Print out a list of formatted and Boxified text to the Console of your youngest and oldest pets
        /// </summary>
        /// <param name="sortedList">List of sorted Pets to print Youngest and Oldest</param>
        public static void PrintAgeStats(List<Pet> sortedList)
        {
            //Since list is sorted we can set youngest age to first in list, and oldest age to last in list
            int youngest = sortedList.First().age;
            int oldest = sortedList.Last().age;

            //Set first name in list to stringYoung and last name in list to stringOld
            string stringYoung = sortedList.First().name;
            string stringOld = sortedList.Last().name;

            //Keep track of how many to make english make sense below
            int youngCount = 1, oldCount = 1;

            //loop the remaining pets and find ages that match youngest or oldest
            for (int i = 1; i < sortedList.Count - 1; i++)
            {
                if (sortedList[i].age == youngest)
                {
                    youngCount++;
                    stringYoung += " & " + sortedList[i].name;
                }
                if (sortedList[i].age == oldest)
                {
                    oldCount++;
                    stringOld += " & " + sortedList[i].name;
                }
            }

            //Prep for random number and cleanup the screen
            Random random = new Random();
            Console.Clear();

            //Create a new list of strings to feed to boxify
            var petAge = new List<string>();

            //Add 2 strings to list.  Uses plurals if count is > 1
            petAge.Add((youngCount > 1) ? stringYoung + " are your youngest pets at " + youngest + " years old" : stringYoung + " is your youngest pet at " + youngest + " years old");
            petAge.Add((oldCount > 1) ? stringOld + " are your oldest pets at " + oldest + " years old" : stringOld + " is your oldest pet at " + oldest + " years old");

            //Find length of longest string in list for formatting
            int length = Boxify.FindLongest(petAge);

            //Write boxified data to the screen using a random border
            Console.WriteLine(Boxify.BoxMe(petAge, length, 'C', random.Next(1, 9)));
            Console.ReadKey();
        }
    }
}
