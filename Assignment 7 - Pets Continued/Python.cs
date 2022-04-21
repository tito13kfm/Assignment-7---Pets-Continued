using System;
using System.Collections.Generic;

namespace Assignment_7___Pets_Continued
{
    internal sealed class Python
    {
        Random random = new Random();
        private List<string> Quotes = new List<string>();

        private static Python internalInstance;
        private Python() { }
        public static Python SharedInstance 
        {
            get 
            { 
                if (internalInstance == null)
                {
                    internalInstance = new Python();
                }
                return internalInstance; 
            } 
        }


        public void RandomQuoteGenerator()
        {
            Quotes.Add("Strange women lying in ponds distributing swords is no basis for a system of government.");

            Quotes.Add("She turned me into a newt.");

            Quotes.Add("Tis but a scratch.");

            Quotes.Add("I fart in your general direction. Your mother was a hamster and your father smelt of elderberries.");

            Quotes.Add("Brother Maynard – bring forth the holy hand grenade!");

            Quotes.Add("It’s only a wafer-thin mint, sir.");

            Quotes.Add("This parrot is no more! It has ceased to be! It’s expired and gone to meet its maker! This is a late parrot!");

            Quotes.Add("Nobody expects the Spanish Inquisition!");

            Quotes.Add("We interrupt this program to annoy you and make things generally more irritating.");

            Quotes.Add("There's nothing wrong with you that an expensive operation can't prolong.");

            Quotes.Add("She's a witch! Burn her already!");

            Quotes.Add("Oh! Now we see the violence inherent in the system! Help, help, I'm being repressed!");

            Quotes.Add("Are you suggesting that coconuts migrate?");
        }

        public String GetRandomQuote()

        {
            int ranNumber = random.Next(0, Quotes.Count - 1);
            var pythonQuote = new List<string>();
            pythonQuote.Add(Quotes[ranNumber]);
            int length = Boxify.FindLongest(pythonQuote);
            string boxedQuote = Boxify.BoxMe(pythonQuote, length, 'C', random.Next(1, 9));
            return boxedQuote;
            //return Quotes[ranNumber];
        }
    }



}
