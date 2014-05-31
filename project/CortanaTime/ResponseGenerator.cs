using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CortanaTime
{
    static class ResponseGenerator
    {
        static DateTime dt;
        
        /// <summary>
        /// checks whether it is am/pm now
        /// </summary>
        /// <param name="dt"></param>
        /// <returns>string containing either "a.m." or "p.m."</returns>
        static private string AnteMeridiem(DateTime dt)
        {
            if (dt.Hour <= 12)
                return "a.m.";
            else
                return "p.m.";
        }

        public static string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }

        /// <summary>
        /// Generates the natural-language response of current time.
        /// </summary>
        /// <returns>string containing natural response.</returns>
        public static string GenerateTimeString()
        {
            dt = DateTime.Now;

            string response = "";
            // Informal language: 
            if (!SettingsManager.UseFormalLanguage)
            {
                response += "It's ";

                // in english it's said "12" instead of "0" at midnight
                if (dt.Hour == 0)
                {
                    dt.AddHours(12);
                }

                if (dt.Minute == 0)
                {
                    response += NumberToWords(dt.Hour % 12);
                }
                else if (dt.Minute == 15)
                {
                    response += "a quarter past " + NumberToWords(dt.Hour);
                }
                else if (dt.Minute == 30)
                {
                    response += "half past " + NumberToWords(dt.Hour % 12);
                }
                else if (dt.Minute == 45)
                {
                    response += "a quarter to " + NumberToWords(dt.Hour % 12 + 1);
                }
                else if ((0 < dt.Minute) && (dt.Minute < 30))
                {
                    response += NumberToWords(dt.Minute) + " past " + NumberToWords(dt.Hour % 12);
                }
                else if ((dt.Minute > 30) && (dt.Minute < 60))
                {
                    response += NumberToWords(60 - dt.Minute) + " to " + NumberToWords(dt.Hour % 12 + 1);
                }

                response += " " + AnteMeridiem(dt);
            }
            else
            {
                response += "It is " + NumberToWords(dt.Hour % 12) + " " + NumberToWords(dt.Minute) + " " + AnteMeridiem(dt);
            }

            return response;
        }

        public static string GetDateString()
        {
            // TODO:
            return "This feature is not available yet.";
        }
    }
}
