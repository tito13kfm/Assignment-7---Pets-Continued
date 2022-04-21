using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Assignment_7___Pets_Continued
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //declare a list of Pet objects
            var petList = new List<Pet>();

            //variable declarations
            int choice;
            string fileName;

            //Let's draw something fancy
            Console.WriteLine(Statics.title);
            Console.WriteLine("Press enter to continue...");
            Console.ReadKey();

            //Clean up our mess
            Console.Clear();

            //Summoning the Pythons
            var monty = Python.SharedInstance;
            monty.RandomQuoteGenerator();

            //Menu of choices for user
            while (true)
            {
                Console.Clear();
                Console.WriteLine("There are currently {0} Pets in the list.", petList.Count);
                Console.WriteLine("1. Add pets");
                if (petList.Count > 0)
                {
                    Console.WriteLine("2. Print Pet summary");
                    Console.WriteLine("3. Print Pet details");
                    Console.WriteLine("4. Print Pet details in age order");
                    Console.WriteLine("5. Print Youngest and Oldest Pets");
                    Console.WriteLine("6. Edit a Pet");
                    Console.WriteLine("7. Remove a Pet");
                    Console.WriteLine("8. Remove all Pets");
                    Console.WriteLine("9. Show a random Monty Python quote");
                }
                Console.WriteLine();
                Console.WriteLine("S to Save Pets to disk\nL to Load Pets from disk\nQ to Quit");
                Console.WriteLine();
                char selection = IO.ReadKey("Make a selection:");
                //Evaluate users choice and do different actions based on response.
                //Don't allow options not presented to work with if statements
                switch (selection)
                {
                    case '1':
                        Pet.AddPets(petList);
                        break;
                    case '2':
                        if (petList.Count > 0)
                        {
                            PrintPetSummary();
                        }
                        break;
                    case '3':
                        if (petList.Count > 0)
                        {
                            PrintPetDetails(petList);
                            Console.ReadKey();
                        }
                        break;
                    case '4':
                        if (petList.Count > 0)
                        {
                            PrintPetDetails(SortListByAge(petList));
                            Console.ReadKey();
                        }
                        break;
                    case '5':
                        if (petList.Count > 0)
                        {
                            //Sort the list first by age then pass that to PrintAgeStats
                            PrintAgeStats(SortListByAge(petList));
                        }
                        break;
                    case '6':
                        if (petList.Count > 0)
                        {
                            choice = SelectPet(petList, "Which Pet do you wish to edit?\n0 to cancel:");
                            if (choice == -1) { break; }
                            petList[choice].EditPet();
                            Pet.UpdateFixed(petList);
                            Pet.UpdateAgeStatics(petList);
                        }
                        break;
                    case '7':
                        if (petList.Count > 0)
                        {
                            choice = SelectPet(petList, "Which Pet do you wish to remove?\n0 to cancel:");
                            if (choice == -1) { break; }
                            petList[choice] = null;
                            petList.Remove(petList[choice]);
                            Pet.UpdateFixed(petList);
                            Pet.UpdateAgeStatics(petList);
                        }
                        break;
                    case '8':
                        if (petList.Count > 0)
                        {
                            bool sure = IO.ReadYesNo("WARNING: This action is irreversable, are you sure?");
                            if (sure)
                            {
                                for (int i = 0; i < petList.Count; i++)
                                {
                                    petList[i] = null;
                                }
                                petList.Clear();
                                Pet.allFixed = true;
                                Pet.sumOfAllPetAges = 0;
                                Pet.totalNumberOfPets = 0;
                            }
                        }
                        break;
                    case '9':
                        if (petList.Count > 0)
                        {
                            Console.Clear();
                            Console.WriteLine(monty.GetRandomQuote());
                            Console.ReadKey(true);
                        }
                        break;

                    case 'S':
                    case 's':
                        fileName = IO.Read("Enter filename to Save to (animalList.bin) ");
                        CheckExist(petList, fileName);
                        break;
                    case 'L':
                    case 'l':
                        ListDirectory();
                        fileName = IO.Read("Enter filename to Load from (animalList.bin) ");
                        petList = LoadList(petList, fileName);
                        Pet.UpdateFixed(petList);
                        Pet.UpdateAgeStatics(petList);
                        break;

                    case 'Q':
                    case 'q':
                        Console.Clear();
                        Console.WriteLine(Statics.hug);
                        Console.ReadKey();
                        Environment.Exit(0);
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
        /// Method to check if file exists, if it does, ask to overwrite and call SaveList
        /// </summary>
        /// <param name="petList">List of Pets you are saving</param>
        /// <param name="fileName">Filename to save to</param>
        private static void CheckExist(List<Pet> petList, string fileName)
        {
            //build the string of where we are checking if the file exists
            string dir = @"c:\temp";
            string saveFile = Path.Combine(dir, fileName);


            if (File.Exists(saveFile))
            {
                //if it exists, ask the user if they want to overwrite
                bool overwrite = IO.ReadYesNo("File " + fileName + " already exists, overwrite? Yes/No");

                //if they answer yes, forward the list and fileName to the SaveList method
                if (overwrite)
                {
                    SaveList(petList, fileName);
                }
                return;
            }

            //if file doesn't exist, assume it's ok to save
            SaveList(petList, fileName);
        }

        /// <summary>
        /// List all .bin files in c:\temp\
        /// </summary>
        private static void ListDirectory()
        {
            string dir = @"c:\temp";

            //enumerate files in c:\test that match *.bin and list them to the string after removing the directory
            var binFiles = Directory.EnumerateFiles(dir, "*.bin");
            foreach (string file in binFiles)
            {
                string fileName = file.Substring(dir.Length + 1);
                Console.WriteLine(fileName);
            }
        }

        /// <summary>
        /// Load a saved list of pets in to the program from .bin format
        /// </summary>
        /// <param name="petList">Existing petList to return if load failed</param>
        /// <param name="fileName">Name of file to attempt to load</param>
        /// <returns></returns>
        private static List<Pet> LoadList(List<Pet> petList, string fileName)
        {
            //build the string for the filename we are trying to load
            string dir = @"c:\temp";
            string loadFile = Path.Combine(dir, fileName);

            //save current List of Pets to loadedList in case file doesn't exist
            var loadedList = new List<Pet>();

            //if the file exists that we want to load.  Read it and save contents to loadedList
            if (File.Exists(loadFile))
            {
                using (Stream stream = File.Open(loadFile, FileMode.Open))
                {
                    var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                    loadedList = (List<Pet>)bformatter.Deserialize(stream);
                }

                return loadedList;
            }
            else
            {
                Console.WriteLine("File not found.  Press any key to continue.");
                Console.ReadKey();
                return petList;
            }
        }


        /// <summary>
        /// Save list of pets to file using binary stream
        /// </summary>
        /// <param name="petList">List of Pets you are saving</param>
        /// <param name="fileName">Filename to save to</param>
        private static void SaveList(List<Pet> petList, string fileName)
        {
            //Build string of file to save
            if(fileName == "") { return; }
            string dir = @"c:\temp";
            string saveFile = Path.Combine(dir, fileName);

            //Write serialized info to binary file
            using (Stream stream = File.Open(saveFile, FileMode.Create))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                bformatter.Serialize(stream, petList);
            }
        }

        /// <summary>
        /// Returns the users choice of which Pet they are selecting
        /// </summary>
        /// <param name="petList">List of Pets to work with</param>
        /// <param name="prompt">Question to ask the user</param>
        /// <returns></returns>
        private static int SelectPet(List<Pet> petList, string prompt)
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
        /// Accept a list of Pets, sort it by age, and return a sorted list
        /// </summary>
        /// <param name="petList">List of Pets to sort</param>
        /// <returns>Sorted List of Pets</returns>
        public static List<Pet> SortListByAge(List<Pet> petList)
        {
            return petList.OrderBy(x => x.GetAge()).ToList();
        }

        /// <summary>
        /// Print out a list of formatted and Boxified text to the Console of your youngest and oldest pets
        /// </summary>
        /// <param name="sortedList">List of sorted Pets to print Youngest and Oldest</param>
        public static void PrintAgeStats(List<Pet> sortedList)
        {
            //Since list is sorted we can set youngest age to first in list, and oldest age to last in list
            int youngest = sortedList.First().GetAge();
            int oldest = sortedList.Last().GetAge();

            //Set first name in list to stringYoung and last name in list to stringOld
            string stringYoung = sortedList.First().GetName();
            string stringOld = sortedList.Last().GetName();

            //Keep track of how many to make english make sense below
            int youngCount = 1, oldCount = 1;

            //loop the remaining pets and find ages that match youngest or oldest
            for (int i = 1; i < sortedList.Count - 1; i++)
            {
                if (sortedList[i].GetAge() == youngest)
                {
                    youngCount++;
                    stringYoung += " & " + sortedList[i].GetName();
                }
                if (sortedList[i].GetAge() == oldest)
                {
                    oldCount++;
                    stringOld += " & " + sortedList[i].GetName();
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
                var s = String.Format("{0, -2} | {1,-15} | {2,-5} | {3,-10} | {4,-10}", i, p.GetName(), p.GetAge(), p.GetBreed(), p.GetSpayed() ? "Yes" : "No ");
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


    }
}
