using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derp2
{
    public class Language : Lazy<LanguageBase>
    {
        public Language(Func<LanguageBase> function) : base(function)
        {
        }

        public Language(LanguageBase languageBase) : base(() => languageBase)
        {
        }
    }
}
