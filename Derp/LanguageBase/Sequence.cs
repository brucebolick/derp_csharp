using System;

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
            if (!Cache.Nullable.ContainsKey(this))
            {
                Cache.CacheMiss++;
                Cache.Nullable[this] = _left.Value.Nullable() && _right.Value.Nullable();
            }
            else
            {
                Cache.CacheHit++;
            }
            return Cache.Nullable[this];
        }

        public override Language Derive(char inputCharacter)
        {
            var key = new Tuple<char, LanguageBase>(inputCharacter, this);

            if (Cache.Derivative.ContainsKey(key))
            {
                Cache.CacheHit++;
                return Cache.Derivative[key];
            }

            Cache.CacheMiss++;

            var derivative = Sequence(Language(() => _left.Value.Derive(inputCharacter).Value), _right);
            
            Cache.Derivative[key] = _left.Value.Nullable()
                ? Language(() => new Or(derivative, Language(() => _right.Value.Derive(inputCharacter).Value)))
                : derivative;

            return Cache.Derivative[key];
        }
    }
}
