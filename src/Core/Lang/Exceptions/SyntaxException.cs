using PHP.Core.Lang.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.Exceptions
{
    public class SyntaxException : Exception
    {
        public readonly TokenItem Token;
        public SyntaxException(string message, TokenItem token) :
            base($"Syntax exception: {message} at line {token.Position.Line + 1}, column {token.Position.Column + 1}") => Token = token;
    }
}
