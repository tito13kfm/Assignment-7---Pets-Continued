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
            //assume first pet object is both youngest and oldest

            //Menu of choices for the user will go here
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
                }
                Console.WriteLine("");
                string selection = IO.Read("Make a selection:");
                switch (selection)
                {
                    case "1":
                        Pet.AddPets(petList);
                        break;
                    case "2":
                        petList[SelectPet(petList)].HappyBirthday();
                        break;
                    case "3":
                        Pet.PrintPetSummary();
                        break;
                    case "4":
                        PrintPetDetails(petList);
                        break;
                    case "5":
                        PrintPetAge(petList);
                        break;
                    case "6":
                        int choice = SelectPet(petList);
                        petList[choice].AddRemove(false);
                        petList.Remove(petList[choice]);
                        Pet.allFixed = true;
                        foreach (Pet pet in petList)
                        {
                            Pet.allFixed = Pet.allFixed & pet.spayed;
                        }
                        break;
                    default:
                        Console.WriteLine("Please make a valid selection");
                        Console.WriteLine("Press Enter to continue");
                        Console.ReadKey();
                        break;
                }

            }


        }

        private static void PrintPetAge(List<Pet> petList)
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
            Console.Clear();
            List<string> petAge = new List<string>();
            petAge.Add((youngCount > 1) ? stringYoung + " are your youngest pets" : stringYoung + " is your youngest pet");
            petAge.Add((oldCount > 1) ? stringOld + " are your oldest pets" : stringOld + " is your oldest pet");

            int length = Boxify.FindLongest(petAge);
            Console.WriteLine(Boxify.BoxMe(petAge, length, 'C', 1));
            Console.ReadKey();
        }

        private static void PrintPetDetails(List<Pet> petList)
        {
            Random random = new Random();
            Console.Clear();
            List<string> petStats = new List<string>();
            foreach (Pet p in petList)
            {
                string s = String.Format("You have a {0} named {1} who is {2} years old and is {3}fixed.", p.breed, p.name, p.age, p.spayed ? "" : "NOT ");
                petStats.Add(s);
            }
            int length = Boxify.FindLongest(petStats);
            Console.WriteLine(Boxify.BoxMe(petStats, length, 'L', random.Next(1,9)));
            Console.ReadKey();
        }

        static int SelectPet(List<Pet> petList)
        {
            Console.Clear();
            for (int i = 0; i < petList.Count; i++)
            {
                Console.WriteLine(i + 1 + ". " + petList[i].name);
            }
            int choice = IO.ReadPosInt("Choose") - 1;
            return choice;

        }
    }
}
