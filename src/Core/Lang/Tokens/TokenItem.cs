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
        public readonly TokenPosition Position;

        public TokenItem(TokenType type, string data, TokenPosition position)
        {
            Type = type;
            Data = data;
            Position = position;
        }

        public override string ToString() => $"TokenItem(Type: {Type}, Data: \"{Data}\", Position: {Position})";
    }
}
