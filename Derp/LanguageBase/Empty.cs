namespace Derp
{
    public class Empty : LanguageBase
    {
        public override bool Nullable() { return false; }
        public override Language Derive(char inputCharacter) { return Empty; }
    }
}
