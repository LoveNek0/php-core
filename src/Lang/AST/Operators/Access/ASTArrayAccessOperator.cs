using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Operators
{
    public class ASTArrayAccessOperator : ASTNode
    {
        public readonly ASTNode Link;
        public readonly ASTNode Index;
        internal ASTArrayAccessOperator(TokenItem token, ASTNode link, ASTNode index) : base(token)
        {
            Link = link;
            Index = index;
        }

        public override string ToString() => $"{Link}[{Index}]";
    }
}