using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derp2
{
    class ItsRaining : LanguageBase
    {
        private Func<bool> _nullable;
        private Func<char, Language> _derive;

        public ItsRaining()
        {
            var ThingsThatItCanRain = Or(
                Literal("real rain"),
                Literal("cats"),
                Literal("dogs"),
                Literal("men"),
                Literal("tacos"),
                Literal("chocolate"),
                Literal("fury")
                );

            var sequenceJoiner = Or(
                Literal(", "),
                Literal(" and ")
                );

            var sentenceStart = Literal("It's raining ");

            var sentenceEnd = Literal(".");

            var sequenceOfThingsThatItCanRain = Or(
                ThingsThatItCanRain,
                Sequence(Repeat(ThingsThatItCanRain, sequenceJoiner), ThingsThatItCanRain)
                );

            var completeSentence = Sequence(
                sentenceStart, sequenceOfThingsThatItCanRain, sentenceEnd
                );
        }

        public override bool Nullable()
        {
            return _nullable();
        }

        public override Language Derive(char inputCharacter)
        {
            return _derive(inputCharacter);
        }

        
    }
}
