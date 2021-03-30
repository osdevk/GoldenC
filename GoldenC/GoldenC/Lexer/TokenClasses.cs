using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenC.Lexer
{
    enum TokenType : int
    {
        SPECIAL = 0,
        STRING = 1,
        NUMBER = 2,
        KEYWORD = 3,
        UNDEF = 2^32
    }
    class Token
    {
        public Tuple<TokenType, object> Content;
        
        public Token(Tuple<TokenType, object> ct) { Content = ct; }
    }
}
