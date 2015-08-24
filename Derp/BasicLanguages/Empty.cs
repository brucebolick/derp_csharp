using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derp
{
    public class Empty : Language
    {
        public Empty(params object[] list) {
        }

        public override bool Nullable() { return false; }

        public override Language Derive(char inputCharacter)
        {
            return Empty;
        }

        public override string ToString()
        {
            return "{}";
        }
    }
}
