using System;
using System.Linq;
using System.Runtime.CompilerServices;

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
            if (!Cache.Nullable.ContainsKey(this))
            {
                Cache.CacheMiss++;
                Cache.Nullable[this] = _left.Value.Nullable() || _right.Value.Nullable();
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

            Cache.Derivative[key] = Language(() => new Or(_left.Value.Derive(inputCharacter), _right.Value.Derive(inputCharacter)));

            return Cache.Derivative[key];
        }
    }
}
