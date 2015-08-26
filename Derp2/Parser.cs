using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derp2
{
    public class Parser
    {
        public static bool Parses(Lazy<Language> langauge, string input)
        {
            langauge = input.Aggregate(langauge, (current, inputCharacter) => current.Value.Derive(inputCharacter));

            return langauge.Value.Nullable();
        }
    }
}
