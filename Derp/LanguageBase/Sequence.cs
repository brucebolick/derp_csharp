namespace Derp
{
    public class Sequence : LanguageBase
    {
        private readonly Language _left;
        private readonly Language _right;

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
            var derivative = Sequence(Language(() => _left.Value.Derive(inputCharacter).Value), _right);
            return _left.Value.Nullable() ? Language(() => new Or(derivative, Language(() => _right.Value.Derive(inputCharacter).Value))) : derivative;
        }
    }
}
