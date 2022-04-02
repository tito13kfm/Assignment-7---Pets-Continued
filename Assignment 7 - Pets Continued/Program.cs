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
                Console.WriteLine("2. Celebrate a birthday");
                Console.WriteLine("");
                int selection = IO.ReadPosInt("Make a selection:");
                switch (selection)
                {
                    case 1:
                        Pet.AddPets(petList);
                        break;
                    case 2:
                        petList[SelectPet(petList)].HappyBirthday();
                        break;
                    default:
                        break;
                }

            }

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
            List<string> petStats = new List<string>();
            foreach (Pet p in petList)
            {
                string s = String.Format("You have a {0} named {1} who is {2} years old and is {3}fixed.", p.breed, p.name, p.age, p.spayed ? "" : "NOT ");
                petStats.Add(s);
            }
            petStats.Add("\n\n");
            petStats.Add((youngCount > 1) ? stringYoung + " are your youngest pets" : stringYoung + " is your youngest pet");
            petStats.Add((oldCount > 1) ? stringOld + " are your oldest pets" : stringOld + " is your oldest pet");

            Pet.PrintPetSummary();
            Console.ReadKey();

                   
            int length = Boxify.FindLongest(petStats);
            Console.WriteLine(Boxify.BoxMe(petStats, length, 'L', 1));
            Console.ReadKey ();
        }

        static int SelectPet(List<Pet> petList)
        {
            Console.Clear ();
            for (int i = 0; i < petList.Count; i++)
            {
                Console.WriteLine(i+1 + ". " + petList[i].name);
            }
            int choice = IO.ReadPosInt("Choose") - 1;
            return choice;

        }
    }
}
