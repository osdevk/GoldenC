using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenC.Lexer
{
    class Lexer
    {
        string txt = "";
        int inx = 0;
        int index = 0;
        Tuple<bool, string> isKeyword()
        {
            string r = "";
            Tuple<bool, string> re = new Tuple<bool, string>(false, "");
            char c = 'h';
            int h = index-1;
            while (char.IsLetter(c) && h < txt.Length)
            {
                c = txt[h];
                r += c;
                h++;
            }
            Console.WriteLine("R: " + r);
            switch (r.ToUpper())
            {
                case "FUNC":
                    re = new Tuple<bool, string>(true, "func");
                    break;
                case "VAR":
                    re = new Tuple<bool, string>(true, "var");
                    break;
                case "START":
                    re = new Tuple<bool, string>(true, "start");
                    break;
                case "IF":
                    re = new Tuple<bool, string>(true, "if");
                    break;
                case "WHILE":
                    re = new Tuple<bool, string>(true, "while");
                    break;
                case "PRINT":
                    re = new Tuple<bool, string>(true, "print");
                    break;
            }
            index = h;
            return re;
        }
        string getString(char c)
        {
            string r = "";
            char startChar = txt[inx - 1];
            char gc = txt[inx];
            while(inx < txt.Length && gc != startChar)
            {
                gc = txt[inx];
                r += gc;
                inx++;
            }
            index = inx;
            return r;
        }
        string getDigit(char c)
        {
            string r = "";
            int l = 0;
            char gc = txt[l];
            while (l < txt.Length && isDigit(gc))
            {
                gc = txt[l];
                r += gc;
                l++;
            }
            index = l;
            return r;
        }
        bool isDigit(char c)
        {
            int t;
            if(int.TryParse(c.ToString(), out t) || c == '.')
            {
                return true;
            }
            return false;
        }
        public List<Token> Process(string text)
        {
            List<Token> result = new List<Token>();
            
            txt = text;
            char c;
            while(index < text.Length)
            {
                c = text[index];
                index++;
                inx = index;
                if (isDigit(c))
                    result.Add(new Token(new Tuple<TokenType, object>(TokenType.NUMBER, getDigit(c))));
                if(c == '"')
                {
                    result.Add(new Token(new Tuple<TokenType, object>(TokenType.STRING, getString(c))));
                }
                if (c == '\'')
                {
                    result.Add(new Token(new Tuple<TokenType, object>(TokenType.STRING, getString(c))));
                }
                Tuple<bool, string> isKey = isKeyword();
                Console.WriteLine("DEBUG: " + "I1: " + isKey.Item1 + ", I2: " + isKey.Item2);
                if (isKey.Item1 == true)
                {
                    Console.WriteLine("DEBUG: " + "I1: " + isKey.Item1 + ", I2: " + isKey.Item2);
                    result.Add(new Token(new Tuple<TokenType, object>(TokenType.KEYWORD, isKey.Item2)));
                }
                switch (c)
                {
                    case '(':
                        result.Add(new Token(new Tuple<TokenType, object>(TokenType.SPECIAL, "(")));
                        break;
                    case ')':
                        result.Add(new Token(new Tuple<TokenType, object>(TokenType.SPECIAL, ")")));
                        break;
                    case '?':
                        result.Add(new Token(new Tuple<TokenType, object>(TokenType.SPECIAL, "(")));
                        break;
                    case ';':
                        result.Add(new Token(new Tuple<TokenType, object>(TokenType.SPECIAL, "(")));
                        break;
                    case ',':
                        result.Add(new Token(new Tuple<TokenType, object>(TokenType.SPECIAL, "(")));
                        break;
                }
            }
            return result;
        }
    }
}
