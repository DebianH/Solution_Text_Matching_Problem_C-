using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CODE_TEST_David_Hernandez
{

    public static class StringExtensions
    {

        public static void Main(string[] args)
        {

            bool useApp = true;
            while (useApp)
            {
                Console.WriteLine("\nPress 'Y' to continue or 'N' to exit: ");
                string confirm = Console.ReadLine().ToLower();

                if (confirm == "y")
                {
                    //Home
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("._____Welcome to_______.");
                    Console.WriteLine("| Search indexOfManual |");
                    Console.WriteLine("------------------------");

                    Console.WriteLine("Enter a text: ");
                    string text = Console.ReadLine().ToLower();

                    Console.WriteLine("Enter a subtext to find index: ");
                    string subtext = Console.ReadLine().ToLower();

                    List<int> result = new List<int>();

                    //First comprobation
                    if (text.AllIndicesOf(subtext).Count() == 0)
                    {
                        Console.WriteLine("no matches");
                    }

                    //Show the index in text
                    foreach (var index in text.AllIndicesOf(subtext))
                    {
                        result.Add(index + 1);
                    }
                    Console.WriteLine(string.Join(", ", result));

                }
                else
                {
                    useApp = false;
                }

            }

        }

        //Implement algorith KMP
        public static IEnumerable<int> AllIndicesOf(this string text, string pattern)
        {
            if (string.IsNullOrEmpty(pattern))
            {
                throw new ArgumentNullException(nameof(pattern));
            }
            return Kmp(text, pattern);
        }

        private static IEnumerable<int> Kmp(string text, string pattern)
        {

            int M = pattern.Length;
            int N = text.Length;

            int[] lps = LongestPrefixSuffix(pattern);
            int i = 0, j = 0;

            while (i < N)
            {
                if (pattern[j] == text[i])
                {
                    j++;
                    i++;
                }
                if (j == M)
                {
                    yield return i - j;
                    j = lps[j - 1];
                }

                else if (i < N && pattern[j] != text[i])
                {
                    if (j != 0)
                    {
                        j = lps[j - 1];
                    }
                    else
                    {
                        i++;
                    }
                }
            }
        }

        private static int[] LongestPrefixSuffix(string pattern)
        {
            int[] lps = new int[pattern.Length];
            int length = 0;
            int i = 1;

            while (i < pattern.Length)
            {
                if (pattern[i] == pattern[length])
                {
                    length++;
                    lps[i] = length;
                    i++;
                }
                else
                {
                    if (length != 0)
                    {
                        length = lps[length - 1];
                    }
                    else
                    {
                        lps[i] = length;
                        i++;
                    }
                }
            }
            //Console.WriteLine(string.Join(", ", lps));
            return lps;
        }


    }
}
