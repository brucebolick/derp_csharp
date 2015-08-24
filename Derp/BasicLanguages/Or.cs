using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derp
{
    public class Or : Language
    {
        private Language _left;
        private Language _right;

        public Or(params object[] list) {
            _left = (Language)list[0];
            _right = (Language)list[1];
        }
        
        public override bool Nullable()
        {
            return _left.Nullable() || _right.Nullable();
        }

        public override Language Derive(char inputCharacter)
        {
            return new Or(
                Lazy(_left.Derive(inputCharacter)), 
                Lazy(_right.Derive(inputCharacter))
            );
        }

        public override string ToString()
        {
            return "(" + _left.ToString() + " or " + _right.ToString() + ")";
        }
    }
}
