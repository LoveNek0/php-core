using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.Tokens
{
    public class TokenItem
    {
        public readonly TokenType Type;
        public readonly string Data;
        public readonly int Line;
        public readonly int Column;
        public readonly int Position;

        public TokenItem(TokenType type, string data, int position, int line, int column)
        {
            Type = type;
            Data = data;
            Line = line;
            Column = column;
            Position = position;
        }

        public override string ToString() => $"TokenItem(Type: {Type}, Data: \"{Data}\", Position: {Line}:{Column})";
    }
}
