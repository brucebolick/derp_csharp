using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derp
{
    public class Literal : Language
    {
        private char _languageCharacter;

        public Literal(params object[] list) {
            _languageCharacter = (char)list[0];
        }

        public override bool Nullable() { return false; }

        public override Language Derive(char inputCharacter)
        {
            if (this._languageCharacter == inputCharacter)
                return Epsilon;
            else
                return Empty;
        }

        public override string ToString()
        {
            return _languageCharacter.ToString();
        }
    }
}
