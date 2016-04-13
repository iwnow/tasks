using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardPath
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("List:\n");
            var cards = CardPath.GenCards();
            foreach (var cardPath in cards)
            {
                Console.WriteLine(cardPath.ToString());
            }

            Console.WriteLine("\n\nLinked:\n");

            var linked = CardPath.LinkCard(cards);
            
            foreach (var c in linked) {
                Console.WriteLine(c.ToString());
            }
        }
    }
}
