using System;
using System.Linq;

namespace Derp
{
    public abstract class LanguageBase
    {
        public abstract bool Nullable();
        public abstract Language Derive(char inputCharacter);

        public static readonly Language Empty = Language(() => new Empty());
        
        public static readonly Language Epsilon = Language(() => new Epsilon());

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

        public static Language Language(Func<LanguageBase> function)
        {
            return new Language(function);
        }

        public static Language Language(LanguageBase languageBase)
        {
            return new Language(languageBase);
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

    public class Epsilon : LanguageBase
    {
        public override bool Nullable() { return true; }
        public override Language Derive(char inputCharacter) { return Empty; }
    }

    public class Empty : LanguageBase
    {
        public override bool Nullable() { return false; }
        public override Language Derive(char inputCharacter) { return Empty; }
    }

    public class Literal : LanguageBase
    {
        private char _languageCharacter;

        public Literal(char languageCharacter)
        {
            _languageCharacter = languageCharacter;
        }

        public override bool Nullable() { return false; }

        public override Language Derive(char inputCharacter)
        {
            return inputCharacter == _languageCharacter ? Epsilon : Empty;
        }
    }

    public class Or : LanguageBase
    {
        private Language _left, _right;

        public Or(params Language[] languages)
        {
            _left = languages[0];
            _right = languages[1];
        }

        public override bool Nullable()
        {
            return _left.Value.Nullable() || _right.Value.Nullable();
        }

        public override Language Derive(char inputCharacter)
        {
            return Language(() => new Or(_left.Value.Derive(inputCharacter), _right.Value.Derive(inputCharacter)));
        }
    }

    public class Sequence : LanguageBase
    {
        private Language _left, _right;

        public Sequence(params Language[] languages)
        {
            _left = languages[0];
            _right = languages[1];
        }

        public override bool Nullable()
        {
            return _left.Value.Nullable() && _right.Value.Nullable();
        }

        public override Language Derive(char inputCharacter)
        {
            Language derivative = Sequence(Language(() => _left.Value.Derive(inputCharacter).Value), _right);

            if (_left.Value.Nullable())
                return Language(() => new Or(derivative, Language(() => _right.Value.Derive(inputCharacter).Value)));
            else
                return derivative;
        }
    }
}
