using System.Collections.Generic;

namespace Assignment_7___Pets_Continued
{
    internal class Boxify
    {
        static int length = 0;
        static string longestString = "";
        static string boxifiedText = "";
        static string topLeft = "", topRight = "", bottomLeft = "", bottomRight = "", vertical = "";
        static char horizontal = ' ';

        public static string BoxMe(List<string> yourText, int length, char alignment, int borderStyle)
        {
            //setup top border
            BorderStyle(borderStyle);
            string border = new string(horizontal, length + 4);
            boxifiedText = topLeft + border + topRight;

            switch (alignment)
            {
                //All the stuff for Left align
                case 'L':
                    //build the text between top and bottom borders.
                    foreach (string s in yourText)
                    {
                        int padLength = border.Length - s.Length - 1;
                        //I don't like it... but you gotta do what you gotta do.
                        //Program crashes if padLength <0.. so we fudge it
                        while (padLength < 0)
                        {
                            padLength++;
                        }
                        string padding = new string(' ', padLength);
                        boxifiedText = boxifiedText + "\n" + vertical + " " + s + padding + vertical;
                    }
                    break;

                //Center align
                case 'C':
                    //formatting broke on odd length inputs, so hacked it to go up to next integer
                    length = length % 2 == 0 ? length : length + 1;
                    border = new string(horizontal, length + 4);

                    //remaking the top border to fit with new length
                    boxifiedText = topLeft + border + topRight;

                    //Building the gooey inards of the box
                    foreach (string s in yourText)
                    {
                        int padLength = (length - s.Length + 4) / 2;
                        while (padLength < 0)
                        {
                            padLength++;
                        }
                        string padding = new string(' ', padLength);

                        //This is the line of code that almost broke me.
                        //If the length of the padding is even, then keep it.  If it's odd, make it even
                        padLength = s.Length % 2 == 0 ? padLength : padLength + 1;
                        string paddingRight = new string(' ', padLength);

                        boxifiedText = boxifiedText + "\n" + vertical + padding + s + paddingRight + vertical;
                    }
                    break;

                //Right align
                case 'R':
                    foreach (string s in yourText)
                    {
                        int padLength = length - s.Length + 2;
                        while (padLength < 0)
                        {
                            padLength++;
                        }
                        string padding = new string(' ', padLength);
                        boxifiedText = boxifiedText + "\n" + vertical + padding + s + "  " + vertical;
                    }
                    break;

                //Should probably make the default do something...
                default:
                    break;
            }

            //stick on the bottom border
            boxifiedText = boxifiedText + "\n" + bottomLeft + border + bottomRight;
            return boxifiedText;
        }

        public static void BorderStyle(int borderStyle)
        {
            switch (borderStyle)
            {
                case 1: // Border Style -1-
                    topLeft = "╔";
                    topRight = "╗";
                    bottomLeft = "╚";
                    bottomRight = "╝";
                    horizontal = '═';
                    vertical = "║";
                    return;

                case 2: // Border Style -2-
                    topLeft = "┌";
                    topRight = "┐";
                    bottomLeft = "└";
                    bottomRight = "┘";
                    horizontal = '─';
                    vertical = "│";
                    return;

                case 3: // Border Style -3-
                    topLeft = "█";
                    topRight = "█";
                    bottomLeft = "█";
                    bottomRight = "█";
                    horizontal = '■';
                    vertical = "█";
                    return;

                case 4: // Border Style -4-
                    topLeft = "░";
                    topRight = "░";
                    bottomLeft = "░";
                    bottomRight = "░";
                    horizontal = '░';
                    vertical = "░";
                    return;

                case 5: // Border Style -5-
                    topLeft = "▓";
                    topRight = "▓";
                    bottomLeft = "▓";
                    bottomRight = "▓";
                    horizontal = '▓';
                    vertical = "▓";
                    return;

                case 6: // Border Style -6-
                    topLeft = "♥";
                    topRight = "♥";
                    bottomLeft = "♥";
                    bottomRight = "♥";
                    horizontal = '♥';
                    vertical = "♥";
                    return;

                case 7: // Border Style -7-
                    topLeft = "♦";
                    topRight = "♦";
                    bottomLeft = "♦";
                    bottomRight = "♦";
                    horizontal = '♦';
                    vertical = "♦";
                    return;

                default:
                    topLeft = "♦";
                    topRight = "♦";
                    bottomLeft = "♦";
                    bottomRight = "♦";
                    horizontal = '♠';
                    vertical = "♣";
                    return;
            }
        }

        public static int FindLongest(List<string> yourText)
        {
            //Find longest string in List yourText
            foreach (string s in yourText)
            {
                //mmmm ternary operator
                longestString = s.Length > longestString.Length ? s : longestString;
            }
            //return length of longest string
            return length = longestString.Length;
        }

    }
}
