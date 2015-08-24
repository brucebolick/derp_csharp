using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derp
{
    public class Parser
    {
        public static bool Parses(Language langauge, string input)
        {
            foreach (char inputCharacter in input)
            {
                langauge = langauge.Derive(inputCharacter);
            }
            return langauge.Nullable();
        }

        public static void PrintWhetherLanguageParsesInput(Language language, string input)
        {
            if (language.Name == null) language.Name = "given";

            if (Parses(language, input))
                Console.WriteLine("The " + language.Name + " language INCLUDES: " + input);
            else
                Console.WriteLine("The " + language.Name + " language EXCLUDES: " + input);
        }
    }
}
