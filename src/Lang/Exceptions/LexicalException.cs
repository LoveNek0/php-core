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
        public readonly int Line;
        public readonly int Column;

        public LexicalException(string message, int line, int column) :
            base($"Lexical exception: {message} at line {line + 1}, column {column + 1}")
        {
            this.Line = line;
            this.Column = column;
        }
    }
}
