using System;
using System.Collections.Generic;
using System.IO;

namespace Assignment_7___Pets_Continued
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //declare a list of Pet objects
            List<Pet> petList = new List<Pet>();

            //variable declarations
            int choice;
            string fileName;

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
                    Console.WriteLine("4. Print Youngest and Oldest Pets");
                    Console.WriteLine("5. Edit a Pet");
                    Console.WriteLine("6. Remove a Pet");
                    Console.WriteLine("7. Remove all Pets");
                }
                Console.WriteLine();
                Console.WriteLine("S to Save Pets to disk\nL to Load Pets from disk\nQ to Quit");
                Console.WriteLine();
                string selection = IO.Read("Make a selection:").ToUpper();

                //Evaluate users choice and do different actions based on response.
                //Don't allow options not presented to work with if statements
                switch (selection)
                {
                    case "1":
                        Pet.AddPets(petList);
                        Pet.UpdateFixed(petList);
                        Pet.UpdateAgeStatics(petList);
                        break;
                    case "2":
                        if (petList.Count > 0)
                        {
                            Pet.PrintPetSummary();
                        }
                        break;
                    case "3":
                        if (petList.Count > 0)
                        {
                            Pet.PrintPetDetails(petList);
                            Console.ReadKey();
                        }
                        break;
                    case "4":
                        if (petList.Count > 0)
                        {
                            Pet.PrintPetAge(petList);
                        }
                        break;
                    case "5":
                        if (petList.Count > 0)
                        {
                            choice = SelectPet(petList, "Which Pet do you wish to edit?");
                            petList[choice].EditPet();
                            Pet.UpdateFixed(petList);
                            Pet.UpdateAgeStatics(petList);
                        }
                        break;
                    case "6":
                        if (petList.Count > 0)
                        {
                            choice = SelectPet(petList, "Select a Pet to remove");
                            petList[choice] = null;
                            petList.Remove(petList[choice]);
                            Pet.UpdateFixed(petList);
                            Pet.UpdateAgeStatics(petList);
                        }
                        break;
                    case "7":
                        if (petList.Count > 0)
                        {
                            petList.Clear();
                            Pet.UpdateAgeStatics(petList);
                        }
                        break;
                    case "S":
                        fileName = IO.Read("Enter filename to Save to (animalList.bin) ");
                        SaveList(petList, fileName);
                        break;
                    case "L":
                        ListDirectory();
                        fileName = IO.Read("Enter filename to Load from (animalList.bin) ");
                        petList = LoadList(petList, fileName);
                        Pet.UpdateFixed(petList);
                        Pet.UpdateAgeStatics(petList);
                        break;
                    case "Q":
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
        /// List all .bin files in c:\temp\
        /// </summary>
        private static void ListDirectory()
        {
            string dir = @"c:\temp";
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
            string dir = @"c:\temp";
            string loadFile = Path.Combine(dir, fileName);

            List<Pet> loadedList = new List<Pet>();
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
            string dir = @"c:\temp";
            string saveFile = Path.Combine(dir, fileName);
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
            Pet.PrintPetDetails(petList);
            int choice = IO.ReadPosInt(prompt) - 1;
            return choice;
        }
    }
}
