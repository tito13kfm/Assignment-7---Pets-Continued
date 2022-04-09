using System;
using System.Collections.Generic;

namespace Assignment_7___Pets_Continued
{
    [Serializable()]
    internal class Pet
    {
        //Variable declarations
        private string name, breed;
        private int age;
        private bool spayed;
        public static int totalNumberOfPets = 0, sumOfAllPetAges = 0;
        public static bool allFixed;

        //Set method
        public void SetPetVars(string name, string breed, int age, bool spayed)
        {
            this.name = name;
            this.breed = breed;
            this.age = age;
            this.spayed = spayed;
        }

        //Get methods
        public string GetName() { return name; }
        public string GetBreed() { return breed; }
        public int GetAge() { return age; }
        public bool GetSpayed() { return spayed; }



        /// <summary>
        /// Prints out a summary of the Pets.  The total number owned, their age total, and the average age.
        /// Also checks if all Pets are fixed and displays message appropriately.
        /// </summary>
        public static void PrintPetSummary()
        {
            //Create a new list of strings to feed to boxify
            List<string> petSummary = new List<string>();

            Console.Clear();

            //Build the strings and add them to the list
            petSummary.Add(String.Format("You have {0} pets", totalNumberOfPets));
            petSummary.Add(String.Format("Their ages add up to {0}", sumOfAllPetAges));
            double sum = (double)sumOfAllPetAges / (double)totalNumberOfPets; // figure out average age
            petSummary.Add(String.Format("Which means their average age is approximately {0}", sum.ToString("#.##"))); // format for 2 decimal points
            petSummary.Add(String.Format((allFixed) ? "Thank you for helping to control the pet population" : "Help control the pet population, have your pets spayed or neutered")); //RIP Bob

            //Determine longest string in list for sizing of box
            int length = Boxify.FindLongest(petSummary);

            //Write the box to the screen
            Console.WriteLine(Boxify.BoxMe(petSummary, length, 'C', 2));
            Console.ReadKey();
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
                string selection = IO.Read("Make a selection or Q to quit:").ToUpper();

                //Edit member variable selected by user
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
                sumOfAllPetAges += p.GetAge();
            }

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
                //instantiate a new Pet object
                Pet newPet = new Pet();

                Console.Clear();

                //Collect info on the new pet
                string name = IO.Read("What is the name of pet #" + (petList.Count + 1) + ":");
                int age = IO.ReadPosInt("How old is " + name + ":");
                string breed = IO.Read("What breed is " + name + ":");
                bool spayed = IO.ReadYesNo("Is " + name + " fixed? (Yes/No):");

                //Call set method to set the member variables
                newPet.SetPetVars(name, breed, age, spayed);

                //Add new pet to list
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
            //assume all pets are fixed, then loop through all pets and prove so or otherwise
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

            //Create a new list of strings to feed to boxify
            List<string> petStats = new List<string>();

            //Create the header separator line
            string line = new string('=', 54);

            //add the strings to the list
            petStats.Add(String.Format("{4, -2} | {0,-15} | {1,-5} | {2,-10} | {3,-10}", "Name", "Age", "Breed", "Fixed?", "#"));
            petStats.Add(line);
            petStats.Add("");
            int i = 0;
            foreach (Pet p in petList)
            {
                i++;
                string s = String.Format("{4, -2} | {0,-15} | {1,-5} | {2,-10} | {3,-10}", p.GetName(), p.GetAge(), p.GetBreed(), p.GetSpayed() ? "Yes" : "No ", i);
                petStats.Add(s);
            }

            //Write the boxified result to the screen
            Console.WriteLine(Boxify.BoxMe(petStats, 52, 'L', 1));
        }


        /// <summary>
        /// Print out a list of formatted and Boxified text to the Console of your youngest and oldest pets
        /// </summary>
        /// <param name="petList">List of Pets to print Youngest and Oldest</param>
        public static void PrintPetAge(List<Pet> petList)
        {
            //assume first pet is youngest and oldest
            int youngest = petList[0].GetAge();
            int oldest = petList[0].GetAge();
            string stringYoung = petList[0].GetName(), stringOld = petList[0].GetName();

            // let's keep a count so we can make the english language make sense below
            int youngCount = 1, oldCount = 1;


            //Loop to check age against assumed youngest and oldest pets.  Build strings based on age of pet being evaluated
            for (int i = 1; i < petList.Count; i++)
            {
                switch (petList[i].GetAge())
                {
                    case int n when n < youngest:
                        stringYoung = petList[i].GetName();
                        youngest = petList[i].GetAge();
                        youngCount = 1;
                        break;
                    case int n when n == youngest:
                        stringYoung = stringYoung + " & " + petList[i].GetName();
                        youngCount++;
                        break;
                    case int n when n > oldest:
                        stringOld = petList[i].GetName();
                        oldest = petList[i].GetAge();
                        oldCount = 1;
                        break;
                    case int n when n == oldest:
                        stringOld = stringOld + " & " + petList[i].GetName();
                        oldCount++;
                        break;
                    default:
                        break;
                }
            }

            //Print the output
            Random random = new Random();
            Console.Clear();

            //Create a new list of strings to feed to boxify
            List<string> petAge = new List<string>();

            //Add 2 strings to list.  Uses plurals if count is > 1
            petAge.Add((youngCount > 1) ? stringYoung + " are your youngest pets" : stringYoung + " is your youngest pet");
            petAge.Add((oldCount > 1) ? stringOld + " are your oldest pets" : stringOld + " is your oldest pet");

            //Find length of longest string in list for formatting
            int length = Boxify.FindLongest(petAge);

            //Write boxified data to the screen using a random border
            Console.WriteLine(Boxify.BoxMe(petAge, length, 'C', random.Next(1, 9)));
            Console.ReadKey();
        }
    }
}
