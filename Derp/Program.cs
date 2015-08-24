using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derp
{
    class Program
    {

        static void Main(string[] args)
        {
            Language space = Language.Literal(" ");

            //One character literal tests
            Language a = new Literal('a'); a.Name = "'a'";
            Parser.PrintWhetherLanguageParsesInput(a, "a"); //true
            Parser.PrintWhetherLanguageParsesInput(a, "b"); //false

            //simple or test
            Language b = new Literal('b'); b.Name = "'b'";
            Language aOrB = new Or(a, b); aOrB.Name = "'a or b'";
            Parser.PrintWhetherLanguageParsesInput(aOrB, "a"); //true
            Parser.PrintWhetherLanguageParsesInput(aOrB, "b"); //true
            Parser.PrintWhetherLanguageParsesInput(aOrB, "c"); //false

            //sequence tests
            Language aaOrb = Language.Sequence(a, aOrB); aaOrb.Name = "'a(a+b)'";
            Parser.PrintWhetherLanguageParsesInput(aaOrb, "aa"); //true
            Parser.PrintWhetherLanguageParsesInput(aaOrb, "ab"); //true
            Parser.PrintWhetherLanguageParsesInput(aaOrb, "abb"); //false

            //multicharacter literal tests
            Language hello = Language.Literal("hello"); hello.Name = "'hello'";
            Parser.PrintWhetherLanguageParsesInput(hello, "hello");
            Parser.PrintWhetherLanguageParsesInput(hello, "olleh");

            //star tests
            Language xy = Language.Star(Language.Sequence(Language.Literal("xy")));

            Console.ReadLine();
        }
    }
}
