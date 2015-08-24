using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derp
{
    public class Epsilon : Language
    {
        public Epsilon(params object[] list) {
        }

        public override bool Nullable() { return true; }

        public override Language Derive(char inputCharacter) {
            return Empty;
        }

        public override string ToString()
        {
            return "{''}";
        }
    }
}
