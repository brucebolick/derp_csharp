using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

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

        public static Language AThroughZ = Or(
            Literal("a"), Literal("g"), Literal("m"), Literal("s"),
            Literal("b"), Literal("h"), Literal("n"), Literal("t"),
            Literal("c"), Literal("i"), Literal("o"), Literal("u"),
            Literal("d"), Literal("j"), Literal("p"), Literal("v"),
            Literal("e"), Literal("k"), Literal("q"), Literal("w"),
            Literal("f"), Literal("l"), Literal("r"), Literal("x"),
            Literal("y"), Literal("z"), Literal("A"), Literal("B"),
            Literal("C"), Literal("I"), Literal("O"), Literal("U"),
            Literal("D"), Literal("J"), Literal("P"), Literal("V"),
            Literal("E"), Literal("K"), Literal("Q"), Literal("W"),
            Literal("F"), Literal("L"), Literal("R"), Literal("X"),
            Literal("G"), Literal("M"), Literal("S"), Literal("Y"),
            Literal("H"), Literal("N"), Literal("T"), Literal("Z")
            );

        public static Language Digit = Or(Literal("0"), Literal("1"), Literal("2"), Literal("3"), Literal("4"),
            Literal("5"), Literal("6"), Literal("7"), Literal("8"), Literal("9"));

        public static Language DigitOneThroughNine = Or(Literal("1"), Literal("2"), Literal("3"), Literal("4"),
            Literal("5"), Literal("6"), Literal("7"), Literal("8"), Literal("9"));

        public static Language Integer = Sequence(ZeroOrOne(Literal("-")), DigitOneThroughNine, Repeat(Digit));
    }
}
