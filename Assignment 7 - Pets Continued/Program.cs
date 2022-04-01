using System;


namespace Assignment_7___Pets_Continued
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int numberPets = IO.ReadPosInt("How many pets do you own?");
            string border = "\n======================================================\n";

            //declare an array of Pet objects of specified size
            Pet[] petArray = new Pet[numberPets];

            //loop to collect data from user
            for (int i = 0; i < numberPets; i++)
            {
                string name = IO.Read("\nWhat is the name of pet #" + (i + 1) + ":");
                int age = IO.ReadPosInt("How old is " + name + ":");
                string breed = IO.Read("What breed is " + name + ":");
                bool spayed = IO.ReadYesNo("Is " + name + " fixed? (Yes/No):");

                //create new instances of Pet object
                petArray[i] = new Pet()
                { name = name, age = age, breed = breed , spayed = spayed};
                Pet.totalNumberOfPets++;
                Pet.sumOfAllPetAges += (int)age; //add the age of current pet to the sume of all pets
                Pet.allFixed = Pet.allFixed & spayed; //This will return false if any single pet isn't fixed
            }

            //assume first pet object is both youngest and oldest
            int youngest = petArray[0].age;
            int oldest = petArray[0].age;
            string stringYoung = petArray[0].name, stringOld = petArray[0].name;

            int youngCount = 1, oldCount = 1; // let's keep a count so we can make the english language make sense below


            //Loop to check age against assumed youngest and oldest pets.  Build strings based on age of pet being evaluated
            for (int i = 1; i < petArray.Length; i++)
            {
                switch (petArray[i].age)
                {
                    case int n when n < youngest:
                        stringYoung = petArray[i].name;
                        youngest = petArray[i].age;
                        youngCount = 1;
                        break;
                    case int n when n == youngest:
                        stringYoung = stringYoung + " & " + petArray[i].name;
                        youngCount++;
                        break;
                    case int n when n > oldest:
                        stringOld = petArray[i].name;
                        oldest = petArray[i].age;
                        oldCount = 1;
                        break;
                    case int n when n == oldest:
                        stringOld = stringOld + " & " + petArray[i].name;
                        oldCount++;
                        break;
                    default:
                        break;

                }

            }

            //Print the output
            Console.Clear();
            Console.WriteLine(border);
            foreach (Pet p in petArray)
            {
                Console.WriteLine("You have a {0} named {1} who is {2} years old and is {3}fixed.", p.breed, p.name, p.age, p.spayed ? "" : "NOT ");
            }
            Console.WriteLine(border);
            Console.WriteLine((youngCount > 1) ? stringYoung + " are your youngest pets" : stringYoung + " is your youngest pet");
            Console.WriteLine((oldCount > 1) ? stringOld + " are your oldest pets" : stringOld + " is your oldest pet");
            Console.WriteLine(border);
            Pet.PrintPetSummary();
            Console.ReadKey();
        }
    }
}
