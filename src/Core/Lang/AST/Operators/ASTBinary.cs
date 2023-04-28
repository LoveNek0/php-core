using PHP.Core.Lang.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.AST.Operators   
{
    public class ASTBinary : ASTNode
    {
        public ASTNode Left => _left;
        public ASTNode Right => _right;

        internal ASTNode _left;
        internal ASTNode _right;
        internal ASTBinary(TokenItem token, ASTNode left = null, ASTNode right = null) : base(token)
        {
            _left = left;
            _right = right;
        }

        public override string ToString() => $"({Left}{Token.Data}{Right})";
    }
}
