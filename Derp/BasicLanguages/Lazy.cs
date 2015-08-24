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
        private Language _retrievedLanguage;

        private void retrieveLanguage()
        {
            if (_retrievedLanguage == null)
            {
                _retrievedLanguage = _storedLanguage;
            }
        }

        public Lazy(Language storedLanguage)
        {
            _storedLanguage = storedLanguage;
        }

        public override bool Nullable()
        {
            retrieveLanguage();
            return _retrievedLanguage.Nullable();
        }

        public override Language Derive(char inputCharacter)
        {
            retrieveLanguage();
            return _retrievedLanguage.Derive(inputCharacter);
        }

        public override string ToString()
        {
            if (_retrievedLanguage == null)
                return "(...)";
            else
                return _retrievedLanguage.ToString();
        }

    }
}
