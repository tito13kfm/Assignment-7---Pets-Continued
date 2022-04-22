using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_7___Pets_Continued
{
    internal static class RandomPet
    {
        /// <summary>
        /// Adds a random pet to the list passed.
        /// </summary>
        /// <param name="petList">List of pets to work with</param>
        public static void AddRandomPet(this List<Pet> petList)
        {
            Random random = new Random();
            string name = Statics.petNames[random.Next(0, Statics.petNames.Count)];
            string breed = Statics.petBreeds[random.Next(0, Statics.petBreeds.Count)];
            int age = random.Next(1, 41);

            //80% chance they are spayed
            bool spayed = random.NextDouble() < 0.8;

            //Call the constructor
            Pet newPet = new Pet(name, breed, age, spayed);

            //Add new pet to list
            petList.Add(newPet);

        }

        /// <summary>
        /// Loads PetNames.txt and AnimalBreeds.txt to lists for random pet generation
        /// </summary>
        public static void LoadNamesAndBreeds()
        {

            foreach (string line in File.ReadLines(@"Data\PetNames.txt", Encoding.UTF8))
            {
                Statics.petNames.Add(line);
            }
            foreach (string line in File.ReadLines(@"Data\AnimalBreeds.txt", Encoding.UTF8))
            {
                Statics.petBreeds.Add(line);
            }
        }
    }
}
