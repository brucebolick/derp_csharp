namespace Derp
{
    public class Literal : LanguageBase
    {
        private readonly char _languageCharacter;

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
}
