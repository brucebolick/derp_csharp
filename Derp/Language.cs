using System;

namespace Derp
{
    public class Language : Lazy<LanguageBase>
    {
        public Language(Func<LanguageBase> function) : base(function) { }
    }
}
