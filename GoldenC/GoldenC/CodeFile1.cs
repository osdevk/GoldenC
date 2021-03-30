using System;
using System.Collections.Generic;
using GoldenC.Lexer;

namespace GoldenC
{
    class Program
    {
        static void Main(string[] args)
        {
            Lexer.Lexer lexer = new Lexer.Lexer();
            List<Token> tkns = lexer.Process("print(\"Hello World\");");
            Console.WriteLine(tkns.Count);
            foreach(var t in tkns)
            {
                Console.WriteLine(t.Content.Item1+", ");
                Console.WriteLine(t.Content.Item2 + "\n");
            }
        }
    }
}