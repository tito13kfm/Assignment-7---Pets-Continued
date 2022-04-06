using System;
using System.Collections.Generic;

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
                Console.WriteLine("");
                string selection = IO.Read("Make a selection:");
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
                        petList.Remove(petList[choice]);
                        Pet.UpdateFixed(petList);
                        break;
                    case "8":
                        petList.Clear();
                        Pet.sumOfAllPetAges = 0;
                        Pet.totalNumberOfPets = 0;
                        break;
                    default:
                        Console.WriteLine("Please make a valid selection");
                        Console.WriteLine("Press Enter to continue");
                        Console.ReadKey();
                        break;
                }

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
