using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_7___Pets_Continued
{
    internal class Menu
    {
        public static void Main(List<Pet> petList)
        {
            //Menu of choices for user
            int choice;
            string fileName;
            //Summoning the Pythons
            var monty = Python.SharedInstance;
            monty.RandomQuoteGenerator();
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
                            Pet.PrintPetSummary();
                        }
                        break;
                    case '3':
                        if (petList.Count > 0)
                        {
                            Pet.PrintPetDetails(petList);
                            Console.ReadKey();
                        }
                        break;
                    case '4':
                        if (petList.Count > 0)
                        {
                            Pet.PrintPetDetails(Pet.SortListByAge(petList));
                            Console.ReadKey();
                        }
                        break;
                    case '5':
                        if (petList.Count > 0)
                        {
                            //Sort the list first by age then pass that to PrintAgeStats
                            Pet.PrintAgeStats(Pet.SortListByAge(petList));
                        }
                        break;
                    case '6':
                        if (petList.Count > 0)
                        {
                            choice = Pet.SelectPet(petList, "Which Pet do you wish to edit?\n0 to cancel:");
                            if (choice == -1) { break; }
                            petList[choice].EditPet();
                            Pet.UpdateFixed(petList);
                            Pet.UpdateAgeStatics(petList);
                        }
                        break;
                    case '7':
                        if (petList.Count > 0)
                        {
                            choice = Pet.SelectPet(petList, "Which Pet do you wish to remove?\n0 to cancel:");
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
                        FileIO.CheckExist(petList, fileName);
                        break;
                    case 'L':
                    case 'l':
                        FileIO.ListDirectory();
                        fileName = IO.Read("Enter filename to Load from (animalList.bin) ");
                        petList = FileIO.LoadList(petList, fileName);
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
    }
}
