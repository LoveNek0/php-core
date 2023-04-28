using PHP.Core.Lang.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.AST
{
    public class ASTBlock : ASTNode
    {
        public ASTNode[] Lines => _lines.ToArray();
        internal List<ASTNode> _lines = new List<ASTNode>(); 
        internal ASTBlock(TokenItem token) : base(token)
        {
        }

        public override string ToString() => $"{{\n{String.Join("\n", _lines)}\n}}";
    }
}
