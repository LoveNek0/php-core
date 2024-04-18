using PHP.Core.Lang.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PHP.Core.Lang.AST.Base;

namespace PHP.Core.Lang.AST.Operators   
{
    public class ASTBinary : ASTNode
    {
        public readonly ASTNode Left;
        public readonly ASTNode Right;

        internal ASTBinary(TokenItem token, ASTNode left = null, ASTNode right = null) : base(token)
        {
            Left = left;
            Right = right;
        }

        public override string ToString() => $"({Left} {Token.Data} {Right})";
    }
}
