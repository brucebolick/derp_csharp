namespace Derp
{
    public class Epsilon : LanguageBase
    {
        public override bool Nullable() { return true; }
        public override Language Derive(char inputCharacter) { return Empty; }
    }
}
