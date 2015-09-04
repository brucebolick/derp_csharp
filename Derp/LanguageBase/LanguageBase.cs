using System;
using System.Linq;

namespace Derp
{
    public abstract class LanguageBase
    {
        public abstract bool Nullable();
        public abstract Language Derive(char inputCharacter);

        public static Language Language(Func<LanguageBase> function)
        {
            return new Language(function);
        }

        public static Language Empty = Language(() => new Empty());

        public static Language Epsilon = Language(() => new Epsilon());

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

        public static Language ZeroOrOne(Language language)
        {
            return Or(Epsilon, language);
        }

        public static Language AnyOfCharacters(string characters)
        {
            return Or(characters.Select(character => Literal(character.ToString())).ToArray());
        }

        public static Language AnyOfStrings(params string[] strings)
        {
            return Or(strings.Select(Literal).ToArray());
        }

        public static Language AThroughZ = AnyOfCharacters("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ");
        public static Language Digit = AnyOfCharacters("0123456789");
        public static Language NonZeroDigit = AnyOfCharacters("123456789");

        public static Language Integer = Sequence(ZeroOrOne(Literal("-")), NonZeroDigit, Repeat(Digit));
    }
}
