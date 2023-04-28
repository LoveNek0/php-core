using PHP.Core.Lang.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.AST
{
    public class ASTNode
    {
        public readonly TokenItem Token;

        protected ASTNode(TokenItem token) => Token = token;

        public override string ToString() => $"{Token.Data}";
    }
}
