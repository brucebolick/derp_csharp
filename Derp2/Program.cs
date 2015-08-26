using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derp2
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = Language.Literal("a");
            
            Console.WriteLine(Parser.Parses(a, "a"));   //True
            Console.WriteLine(Parser.Parses(a, "b"));   //False

            var b = Language.Literal("b");
            var or = Language.Or(a, b);
            
            Console.WriteLine(Parser.Parses(or, "a"));  //True
            Console.WriteLine(Parser.Parses(or, "b"));  //True
            Console.WriteLine(Parser.Parses(or, "c"));  //False, only accepts "a" or "b"

            var hello = Language.Literal("hello");

            Console.WriteLine(Parser.Parses(hello, "hello"));   //True
            Console.WriteLine(Parser.Parses(hello, "goodbye")); //False, only "hello" accepted by 'hello' language

            var space = Language.Literal(" ");
            var goodbye = Language.Literal("goodbye");

            var helloGoodbye = Language.Sequence(hello, space, goodbye);
            var goodbyeHello = Language.Sequence(goodbye, space, hello);

            var eitherOrderOrA = Language.Or(helloGoodbye, goodbyeHello, a);

            Console.WriteLine(Parser.Parses(eitherOrderOrA, "hello goodbye"));  //True
            Console.WriteLine(Parser.Parses(eitherOrderOrA, "goodbye hello"));  //True
            Console.WriteLine(Parser.Parses(eitherOrderOrA, "a"));              //True
            Console.WriteLine(Parser.Parses(eitherOrderOrA, "goodbyehello"));   //False, missing space
            Console.WriteLine(Parser.Parses(eitherOrderOrA, "aa"));             //False, only single a accepted by 'a' language

            var aStar = Language.Repeat(a);
            Console.WriteLine(Parser.Parses(aStar, ""));
            Console.WriteLine(Parser.Parses(aStar, "a"));
            Console.WriteLine(Parser.Parses(aStar, "aa"));
            Console.WriteLine(Parser.Parses(aStar, "aaa"));
            Console.WriteLine(Parser.Parses(aStar, "aaba"));

            var orStar = Language.Repeat(or);
            Console.WriteLine(Parser.Parses(orStar, "aaba"));

            Console.ReadLine();

        }
    }
}
