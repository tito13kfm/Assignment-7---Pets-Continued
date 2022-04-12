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
        private bool spayed = true;
        public static int totalNumberOfPets = 0, sumOfAllPetAges = 0;
        public static bool allFixed;

        //Set method
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

        //Get methods
        public string GetName() { return name; }
        public string GetBreed() { return breed; }
        public int GetAge() { return age; }
        public bool GetSpayed() { return spayed; }

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
                string selection = IO.Read("Make a selection or Q to quit:").ToUpper();

                //Edit member variable selected by user
                switch (selection)
                {
                    case "1":
                        //Next 3 Console.WriteLines show off 3 different ways to do the same thing
                        Console.WriteLine($"Current Name is {this.name}");
                        this.name = IO.Read("Enter new name:");
                        break;
                    case "2":
                        Console.WriteLine("Current Age is {0}", this.age);
                        this.age = IO.ReadPosInt("Enter new age:");
                        break;
                    case "3":
                        Console.WriteLine("Current Breed is " + this.breed);
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
    }
}
