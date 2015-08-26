using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Derp2
{
    public abstract class Language
    {
        public abstract bool Nullable();
        public abstract Lazy<Language> Derive(char inputCharacter);

        public static readonly Lazy<Language> Empty = new Lazy<Language>(() => new Empty());
        public static readonly Lazy<Language> Epsilon = new Lazy<Language>(() => new Epsilon());

        public static Lazy<Language> Literal(string inputString)
        {
            List<Lazy<Language>> sequence =
                inputString.Select(characterForNewClosure
                    => new Lazy<Language>(()
                        => new Literal(characterForNewClosure))).ToList();

            sequence.Add(Epsilon);

            return Sequence(sequence.ToArray());
        }
        public static Lazy<Language> Or(params Lazy<Language>[] languages)
        {
            return languages.Aggregate((a, b) => new Lazy<Language>( () => new Or(a, b)));
        }
        public static Lazy<Language> Sequence(params Lazy<Language>[] languages) 
        {
            return languages.ToList().Aggregate((a, b) => { return new Lazy<Language>(() => new Sequence(a, b)); });
        }

/*
    def rep(parser):
        r = lazy(lambda: alt(epsilon, seq(parser, r)))
        return r
 */

        public static Lazy<Language> Repeat(params Lazy<Language>[] languages)
        {
            var language = Sequence(languages);
            return repeatHelper(language);
        }

        private static Lazy<Language> repeatHelper(Lazy<Language> language)
        {
            return new Lazy<Language>( () => new Or(Epsilon, Sequence(language, repeatHelper(language))));
        }
        
    }

    public class Epsilon : Language
    {
        public override bool Nullable() { return true; }
        public override Lazy<Language> Derive(char inputCharacter) { return Empty; }
    }

    public class Empty : Language
    {
        public override bool Nullable() { return false; }
        public override Lazy<Language> Derive(char inputCharacter) { return Empty; }
    }

    public class Literal : Language
    {
        private char _languageCharacter;

        public Literal(char languageCharacter)
        {
            _languageCharacter = languageCharacter;
        }

        public override bool Nullable() { return false; }

        public override Lazy<Language> Derive(char inputCharacter)
        {
            return inputCharacter == _languageCharacter ? Epsilon : Empty;
        }
    }

    public class Or : Language
    {
        private Lazy<Language> _left, _right;

        public Or(params Lazy<Language>[] languages)
        {
            _left = languages[0];
            _right = languages[1];
        }

        public override bool Nullable()
        {
            return _left.Value.Nullable() || _right.Value.Nullable();
        }

        public override Lazy<Language> Derive(char inputCharacter)
        {
            return new Lazy<Language>(() => new Or(_left.Value.Derive(inputCharacter), _right.Value.Derive(inputCharacter)));
        }
    }

    public class Sequence : Language
    {
        private Lazy<Language> _left, _right;

        public Sequence(params Lazy<Language>[] languages)
        {
            _left = languages[0];
            _right = languages[1];
        }

        public override bool Nullable()
        {
            return _left.Value.Nullable() && _right.Value.Nullable();
        }

        public override Lazy<Language> Derive(char inputCharacter)
        {
            Lazy<Language> derivative = Sequence(new Lazy<Language>(() => _left.Value.Derive(inputCharacter).Value), _right);

            if (_left.Value.Nullable())
                return new Lazy<Language>(() => new Or(derivative, new Lazy<Language>(() => _right.Value.Derive(inputCharacter).Value)));
            else
                return derivative;
        }
    }
}
