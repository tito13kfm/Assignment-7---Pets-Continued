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
                    Console.WriteLine("6. Remove a Pet");
                    Console.WriteLine("7. Remove all Pets");
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
                        petList[SelectPet(petList)].HappyBirthday();
                        break;
                    case "3":
                        Pet.PrintPetSummary();
                        break;
                    case "4":
                        Pet.PrintPetDetails(petList);
                        break;
                    case "5":
                        Pet.PrintPetAge(petList);
                        break;
                    case "6":
                        int choice = SelectPet(petList);
                        petList[choice].AddRemove(false);
                        petList.Remove(petList[choice]);
                        Pet.UpdateFixed(petList);
                        break;
                    case "7":
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

        private static int SelectPet(List<Pet> petList)
        {
            Console.Clear();
            for (int i = 0; i < petList.Count; i++)
            {
                Console.WriteLine("{0}. {1} - {2} year old {3}", i + 1, petList[i].name, petList[i].age, petList[i].breed);
            }
            int choice = IO.ReadPosInt("Choose") - 1;
            return choice;

        }
    }
}
