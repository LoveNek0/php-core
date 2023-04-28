using PHP.Core.Lang.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.Exceptions
{
    public class LexicalException : Exception
    {
        public readonly TokenPosition Position;
        public LexicalException(string message, TokenPosition position) :
            base($"Lexical exception: {message} at line {position.Line + 1}, column {position.Column + 1}") => Position = position;
    }
}
