using System;
using System.Collections.Generic;
using System.Linq;

namespace Derp
{
    public abstract class LanguageBase
    {
        public abstract bool Nullable();
        public abstract Language Derive(char inputCharacter);

        protected static Language Language(Func<LanguageBase> function)
        {
            return new Language(function);
        }

        protected static readonly Language Empty = Language(() => new Empty());

        protected static readonly Language Epsilon = Language(() => new Epsilon());

        public static Dictionary<char, Dictionary<LanguageBase, LanguageBase>> DerivativeCache = new Dictionary<char, Dictionary<LanguageBase, LanguageBase>>();

        public static Dictionary<LanguageBase, bool> NullableCache = new Dictionary<LanguageBase, bool>(); 

        public static Language Literal(string inputString)
        {
            var sequence = inputString.Select(characterForNewClosure => Language( () => new Literal(characterForNewClosure))).ToList();

            sequence.Add(Epsilon);

            return Sequence(sequence.ToArray());
        }
        
        public static Language Or(params Language[] languages)
        {
            return languages.Aggregate((a, b) => Language( () => new Or(a, b)));
        }
        
        public static Language Sequence(params Language[] languages) 
        {
            return languages.ToList().Aggregate((a, b) => { return Language(() => new Sequence(a, b)); });
        }
        
        public static Language Repeat(params Language[] languages)
        {
            var language = Sequence(languages);
            return RepeatHelper(language);
        }

        private static Language RepeatHelper(Language language)
        {
            return Language( () => new Or(Epsilon, Sequence(language, RepeatHelper(language))));
        }
        
    }
}
