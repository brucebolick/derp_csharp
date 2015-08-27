using System;

namespace Derp
{
    static class Program
    {
        private static void Main(string[] args)
        {
            RunRudimentaryTests();
        }

        private static void RunRudimentaryTests()
        {
            var a = LanguageBase.Literal("a");

            Console.WriteLine(Parser.Parses(a, "a"));   //True
            Console.WriteLine(Parser.Parses(a, "b"));   //False

            var b = LanguageBase.Literal("b");
            var or = LanguageBase.Or(a, b);

            Console.WriteLine(Parser.Parses(or, "a"));  //True
            Console.WriteLine(Parser.Parses(or, "b"));  //True
            Console.WriteLine(Parser.Parses(or, "c"));  //False, only accepts "a" or "b"

            var hello = LanguageBase.Literal("hello");

            Console.WriteLine(Parser.Parses(hello, "hello"));   //True
            Console.WriteLine(Parser.Parses(hello, "goodbye")); //False, only "hello" accepted by 'hello' language

            var space = LanguageBase.Literal(" ");
            var goodbye = LanguageBase.Literal("goodbye");

            var helloGoodbye = LanguageBase.Sequence(hello, space, goodbye);
            var goodbyeHello = LanguageBase.Sequence(goodbye, space, hello);

            var eitherOrderOrA = LanguageBase.Or(helloGoodbye, goodbyeHello, a);

            Console.WriteLine(Parser.Parses(eitherOrderOrA, "hello goodbye"));  //True
            Console.WriteLine(Parser.Parses(eitherOrderOrA, "goodbye hello"));  //True
            Console.WriteLine(Parser.Parses(eitherOrderOrA, "a"));              //True
            Console.WriteLine(Parser.Parses(eitherOrderOrA, "goodbyehello"));   //False, missing space
            Console.WriteLine(Parser.Parses(eitherOrderOrA, "aa"));             //False, only single a accepted by 'a' language

            var aStar = LanguageBase.Repeat(a);
            Console.WriteLine(Parser.Parses(aStar, ""));
            Console.WriteLine(Parser.Parses(aStar, "a"));
            Console.WriteLine(Parser.Parses(aStar, "aa"));
            Console.WriteLine(Parser.Parses(aStar, "aaa"));
            Console.WriteLine(Parser.Parses(aStar, "aaba"));

            var orStar = LanguageBase.Repeat(or);
            Console.WriteLine(Parser.Parses(orStar, "aaba"));

            Console.ReadLine();
        }
    }
}
