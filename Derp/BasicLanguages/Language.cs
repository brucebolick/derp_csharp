using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derp
{
    public abstract class Language
    {
        public string Name;
        public abstract Language Derive(char inputCharacter);
        public abstract bool Nullable();
        public override string ToString() { return ""; }

        public static Empty Empty = new Empty();
        
        public static Epsilon Epsilon = new Epsilon();

        public static Language Literal(string inputString)
        {
            var sequence = inputString
                .Select(c => new Literal(c))
                .Concat(new List<Language>() { Epsilon })
                .ToArray();
            return Sequence(sequence);
        }

        public static Language Or(params Language[] languages)
        {
            return languages.Aggregate((a, b) => { return new Or(a, b); });
        }

        public static Language Sequence(params Language[] languages)
        {
            return languages.ToList().Aggregate((a, b) => { return new Sequence(a, b); });
        }

        public static Language Lazy(Language language)
        {
            return new Lazy(language);
        }

        public static Language Star(Language language)
        {
            Lazy recursive;
            
            
        }
    }
}
