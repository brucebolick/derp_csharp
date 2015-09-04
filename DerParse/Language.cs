using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DerParse
{
    public abstract class Language
    {
        public abstract Lazy<bool> Nullable();
        public abstract Lazy<Language> Derive(char inputCharacter);

        public static Lazy<Language> Create(Func<Language> function)
        {
            return new Lazy<Language>(function);
        }
    }
}
