using System;
using System.Collections.Generic;

namespace Derp
{
    public static class Cache
    {
        public static Dictionary<LanguageBase, bool> Nullable = new Dictionary<LanguageBase, bool>();
        public static Dictionary<Tuple<char, LanguageBase>, Language> Derivative = new Dictionary<Tuple<char, LanguageBase>, Language>();
        public static int CacheMiss = 0;
        public static int CacheHit = 0;
    }
}
