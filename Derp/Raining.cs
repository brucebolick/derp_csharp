using System;
using System.Threading;

namespace Derp
{
    public class Raining : LanguageBase
    {
        private Func<bool> _nullable;
        private Func<char, Language> _derive;

 
        public Raining()
        {
            RainingTest();
        }

        public override bool Nullable()
        {
            return false;
        }

        public override Language Derive(char inputCharacter)
        {
            return _derive(inputCharacter);
        }

        public void RainingTest()
        {
            var ThingsItCanRain = AnyOfStrings("real rain", "cats", "dogs", "men", "tacos", "chocolate", "acid rain",
                "fury");

            var SequenceJoiners = AnyOfStrings(", ", " and ", ", and ");
                
            var SequenceOfThingsItCanRain = Or(
                Epsilon,
                ThingsItCanRain,
                Sequence(Repeat(ThingsItCanRain, SequenceJoiners), ThingsItCanRain));

            var Start = Literal("It's raining ");
            var End = Literal(".");

            var Language = Sequence(Start, SequenceOfThingsItCanRain, End);

            Parser.DisplayParseResult(Language, "It's raining tacos.");
            Parser.DisplayParseResult(Language, "It's raining cats and dogs.");
            Parser.DisplayParseResult(Language, "It's raining chocolate, cats and fury.");
            Parser.DisplayParseResult(Language, "It's raining puppies and acid rain.");
            Console.WriteLine("Cache misses: " + Cache.CacheMiss);
            Console.WriteLine("Cache hits: " + Cache.CacheHit);
            Console.ReadLine();

        }
    }
}
