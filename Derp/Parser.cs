﻿using System;
using System.Linq;

namespace Derp
{
    public static class Parser
    {
        public static bool Parses(Language langauge, string input)
        {
            langauge = input.Aggregate(langauge, (current, inputCharacter) => current.Value.Derive(inputCharacter));

            return langauge.Value.Nullable();
        }

        public static void DisplayParseResult(Language language, string input)
        {
            if (Parses(language, input))
            {
                Console.WriteLine("Yes, the language includes the following: " + input);
            }
            else
            {
                Console.WriteLine("No,  the language excludes the following: " + input);
            }
        }

        public static void Assert(bool actual, bool expected, string message = null)
        {
            if (expected != actual)
            {
                throw new Exception(message ?? "Test Failed");
            }
        }
    }
}
