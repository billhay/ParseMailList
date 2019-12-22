namespace ParseMail
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Mail;

    class Program
    {
        private static string InputFileName = @"C:\Users\billh\OneDrive\Documents\Documents2\Eldenberg\olyPCamailinglist.txt";

        private static string OutputFileName = @"C:\Users\billh\OneDrive\Documents\Documents2\Eldenberg\olyPCamailinglist.csv";

        static void Main()
        {
            using var writer = File.CreateText(OutputFileName);

            File.ReadAllText(InputFileName)                    // read entire txt file as a string
            .Split(new[]{ ';', '\n', '\r' })            // split into an array of strings, splitting on ';', newline and return  
            .Where(x => !string.IsNullOrWhiteSpace(x))   // remove any empty strings
            .Select(ToMail)                                   // convert from the input string to a MailAddress object
            .Where(x => x != null)                   // any bad addresses will be returned as nulls - this removes them
            .Select(Email2Csv)                                // change the address to a csv string with three fields
            .WriteLines(writer);                              // write to the output file
        }

        /// <summary>
        /// Parses input string to AddressObject
        /// </summary>
        /// <param name="s">The input string</param>
        /// <returns>The Address object, or null if the parse failed</returns>
        static MailAddress ToMail(string s)
        {
            try
            {
                return new MailAddress(s);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception {0}", e);
            }

            return null;
        }

        /// <summary>
        /// Converts the Address object to our desired (and rather wonky) CSV format
        /// </summary>
        /// <param name="address">The address object</param>
        /// <returns>The address as one line of a csv file</returns>
        private static string Email2Csv(MailAddress address)
        {
            try
            {
                return @$"{CsvQuote(address)},""{address.DisplayName}"",{address.Address}";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            static string CsvQuote(MailAddress address)
            {
                return string.IsNullOrWhiteSpace(address.DisplayName)
                    ? address.Address
                    : $@"""""""{address.DisplayName}"""" <{address.Address}> """;
            }
        }
    }

    public static class Extensions
    {
        /// <summary>
        /// Extension method to write one line at a time to an output stream
        /// </summary>
        /// <typeparam name="T">Type of object being written</typeparam>
        /// <param name="lines">An IEnumerable sequence of objects to be written</param>
        /// <param name="tw">The output TextWriter</param>
        public static void WriteLines<T>(this IEnumerable<T> lines, TextWriter tw)
        {
            foreach (var line in lines)
            {
                tw.WriteLine(line);
            }
        }
    }

}
