using System;
using System.Collections.Generic;

namespace Assignment_7___Pets_Continued
{
    [Serializable()]
    internal class Pet
    {
        public string name, breed;
        public int age;
        public bool spayed;
        public static int totalNumberOfPets = 0, sumOfAllPetAges = 0;
        public static bool allFixed = true; // need to declare this true by default so any single false will make it false

        /// <summary>
        /// Prints out a summary of the Pets.  The total number owned, their age total, and the average age.
        /// Also checks if all Pets are fixed and displays message appropriately.
        /// </summary>
        public static void PrintPetSummary()
        {
            List<string> petSummary = new List<string>();
            Console.Clear();
            petSummary.Add(String.Format("You have {0} pets", totalNumberOfPets));
            petSummary.Add(String.Format("Their ages add up to {0}", sumOfAllPetAges));
            double sum = (double)sumOfAllPetAges / (double)totalNumberOfPets; // figure out average age
            petSummary.Add(String.Format("Which means their average age is approximately {0}", sum.ToString("#.##"))); // format for 2 decimal points
            petSummary.Add(String.Format((allFixed) ? "Thank you for helping to control the pet population" : "Help control the pet population, have your pets spayed or neutered")); //RIP Bob
            int length = Boxify.FindLongest(petSummary);
            Console.WriteLine(Boxify.BoxMe(petSummary, length, 'C', 2));
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

        public void EditPet()
        {
            bool editdone = false;
            while (!editdone)
            {
                Console.Clear();
                Console.WriteLine("Editing {0}", this.name);
                Console.WriteLine("1. Change Name");
                Console.WriteLine("2. Change Age");
                Console.WriteLine("3. Change Breed");
                Console.WriteLine("4. Change Fixed status");

                Console.WriteLine();
                string selection = IO.Read("Make a selection or Q to quit:").ToUpper();
                switch (selection)
                {
                    case "1":
                        Console.WriteLine("Current Name is {0}", this.name);
                        this.name = IO.Read("Enter new name:");
                        break;
                    case "2":
                        Console.WriteLine("Current Age is {0}", this.age);
                        this.age = IO.ReadPosInt("Enter new age:");
                        break;
                    case "3":
                        Console.WriteLine("Current Breed is {0}", this.breed);
                        this.breed = IO.Read("Enter new breed:");
                        break;
                    case "4":
                        string s = this.spayed ? "Unfixing " + this.name + " somehow..." : "Fixing " + this.name + ".";
                        this.spayed = this.spayed ? false : true;
                        Console.WriteLine(s);
                        Console.ReadKey();
                        break;
                    case "Q":
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
            bool done = false;
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
            Console.Clear();
            List<string> petStats = new List<string>();
            string line = new string('=', 54);
            petStats.Add(String.Format("{4, -2} | {0,-15} | {1,-5} | {2,-10} | {3,-10}", "Name", "Age", "Breed", "Fixed?", "#"));
            petStats.Add(line);
            petStats.Add("");
            int i = 0;
            foreach (Pet p in petList)
            {
                i++;
                string s = String.Format("{4, -2} | {0,-15} | {1,-5} | {2,-10} | {3,-10}", p.name, p.age, p.breed, p.spayed ? "Yes" : "No ", i);
                petStats.Add(s);
            }

            Console.WriteLine(Boxify.BoxMe(petStats, 52, 'L', 1));
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
            Random random = new Random();
            Console.Clear();
            List<string> petAge = new List<string>();
            petAge.Add((youngCount > 1) ? stringYoung + " are your youngest pets" : stringYoung + " is your youngest pet");
            petAge.Add((oldCount > 1) ? stringOld + " are your oldest pets" : stringOld + " is your oldest pet");

            int length = Boxify.FindLongest(petAge);
            Console.WriteLine(Boxify.BoxMe(petAge, length, 'C', random.Next(1, 9)));
            Console.ReadKey();
        }
    }
}
