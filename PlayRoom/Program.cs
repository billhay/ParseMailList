using System;

namespace PlayRoom
{
    using System.ComponentModel;
    using System.Text.RegularExpressions;

    class Program
    {
        static void Main()
        {
            (string firstName, string lastName) x;

            x = ParseDisplayName("  smith, fred");
            Console.WriteLine($"First: '{x.firstName}' Last: '{x.lastName}'");

            x = ParseDisplayName(" fred smith ");
            Console.WriteLine($"First: '{x.firstName}' Last: '{x.lastName}'");

            x = ParseDisplayName("  fred ");
            Console.WriteLine($"First: '{x.firstName}' Last: '{x.lastName}'");

            x = ParseDisplayName("");
            Console.WriteLine($"First: '{x.firstName}' Last: '{x.lastName}'");

            x = ParseDisplayName("fred smith Jr");
            Console.WriteLine($"First: '{x.firstName}' Last: '{x.lastName}'");

        }





        static (string firstName, string lastName) ParseDisplayName(string displayName)
        {
            string[] bits = displayName.Split(',');
            if (bits.Length == 2)
            {
                displayName = bits[1] + " " + bits[0];
            }

            bits = displayName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return bits.Length switch
            {
                0 => (string.Empty, string.Empty),
                1 => (bits[0], string.Empty),
                _ => (bits[0], bits[1])
            };

        }
    }
}


