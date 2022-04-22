using System;
using System.Collections.Generic;
using System.IO;

namespace Assignment_7___Pets_Continued
{
    internal class FileIO
    {
        /// <summary>
        /// Load a saved list of pets in to the program from .bin format
        /// </summary>
        /// <param name="petList">Existing petList to return if load failed</param>
        /// <param name="fileName">Name of file to attempt to load</param>
        /// <returns></returns>
        public static List<Pet> LoadList(List<Pet> petList, string fileName)
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
        public static void SaveList(List<Pet> petList, string fileName)
        {
            //Build string of file to save
            if (fileName == "") { return; }
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
        /// Method to check if file exists, if it does, ask to overwrite and call SaveList
        /// </summary>
        /// <param name="petList">List of Pets you are saving</param>
        /// <param name="fileName">Filename to save to</param>
        public static void CheckExist(List<Pet> petList, string fileName)
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
        public static void ListDirectory()
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

    }
}
