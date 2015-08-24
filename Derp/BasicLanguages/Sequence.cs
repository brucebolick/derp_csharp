using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derp
{
    public class Sequence : Language
    {
        private Language _left;
        private Language _right;

        public Sequence(params object[] list) {
            _left = (Language)list[0];
            _right = (Language)list[1];
        }
        
        public override bool Nullable()
        {
            return _left.Nullable() && _right.Nullable();
        }

        public override Language Derive(char inputCharacter)
        {
            Language derivative = new Sequence(Lazy(_left.Derive(inputCharacter)), _right);

            if (_left.Nullable())
                return new Or(derivative, Lazy(_right.Derive(inputCharacter)));
            else
                return derivative;
        }

        public override string ToString()
        {
            return _left.ToString() +  _right.ToString();

        }
    }
}
