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
                    Console.WriteLine("2. Celebrate a birthday");
                    Console.WriteLine("3. Print Pet summary");
                    Console.WriteLine("4. Print Pet details");
                    Console.WriteLine("5. Print Youngest and Oldest Pets");
                    Console.WriteLine("6. Edit a Pet");
                    Console.WriteLine("7. Remove a Pet");
                    Console.WriteLine("8. Remove all Pets");
                }
                Console.WriteLine();
                Console.WriteLine("S to Save Pets to disk, L to Load Pets from disk");
                Console.WriteLine();
                string selection = IO.Read("Make a selection:").ToUpper();
                switch (selection)
                {
                    case "1":
                        Pet.AddPets(petList);
                        Pet.UpdateFixed(petList);
                        break;
                    case "2":
                        choice = SelectPet(petList, "Who is having the birthday?");
                        petList[choice].HappyBirthday();
                        break;
                    case "3":
                        Pet.PrintPetSummary();
                        break;
                    case "4":
                        Pet.PrintPetDetails(petList);
                        Console.ReadKey();
                        break;
                    case "5":
                        Pet.PrintPetAge(petList);
                        break;
                    case "6":
                        choice = SelectPet(petList, "Which Pet do you wish to edit?");
                        petList[choice].EditPet();
                        Pet.UpdateFixed(petList);
                        break;
                    case "7":
                        choice = SelectPet(petList, "Select a Pet to remove");
                        petList[choice].AddRemove(false);
                        petList[choice] = null;
                        petList.Remove(petList[choice]);
                        Pet.UpdateFixed(petList);
                        break;
                    case "8":
                        petList.Clear();
                        Pet.sumOfAllPetAges = 0;
                        Pet.totalNumberOfPets = 0;
                        break;
                    case "S":
                        fileName = IO.Read("Enter filename to Save to (animalList.bin) ");
                        SaveList(petList, fileName);
                        break;
                    case "L":
                        fileName = IO.Read("Enter filename to Load from (animalList.bin) ");
                        petList = LoadList(petList, fileName);
                        break;
                    default:
                        Console.WriteLine("Please make a valid selection");
                        Console.WriteLine("Press Enter to continue");
                        Console.ReadKey();
                        break;
                }

            }


        }

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

        private static int SelectPet(List<Pet> petList, string prompt)
        {
            Pet.PrintPetDetails(petList);
            int choice = IO.ReadPosInt(prompt) - 1;
            return choice;
        }
    }
}
