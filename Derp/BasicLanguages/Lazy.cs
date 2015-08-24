using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derp
{
    class Lazy : Language
    {
        private Language _storedLanguage;

        private Func<bool> _nullable;
        private Func<char, Language> _derive;
        private Func<string> _toString = () => { return ("(...)"); };

        public Lazy(Language storedLanguage)
        {
            _storedLanguage = storedLanguage;
        }

        public override bool Nullable()
        {
            if (_nullable == null)
                _nullable = _storedLanguage.Nullable;
            return _nullable();
        }

        public override Language Derive(char inputCharacter)
        {
            if(_derive == null)
                _derive = _storedLanguage.Derive;
            return _derive(inputCharacter);
        }

        public override string ToString()
        {
            if (_nullable != null || _derive != null)
                _toString = _storedLanguage.ToString;
            return _toString();
        }

    }
}
