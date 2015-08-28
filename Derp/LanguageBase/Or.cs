namespace Derp
{
    public class Or : LanguageBase
    {
        private readonly Language _left;
        private readonly Language _right;

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
}
