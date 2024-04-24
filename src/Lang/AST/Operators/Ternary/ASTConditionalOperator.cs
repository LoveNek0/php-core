using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Operators.Ternary
{
    public class ASTConditionalOperator : ASTNode
    {
        public readonly ASTNode Condition;
        public readonly ASTNode Left;
        public readonly ASTNode Right;
        internal ASTConditionalOperator(TokenItem token, ASTNode condition = null, ASTNode left = null, ASTNode right = null) : base(token)
        {
            Condition = condition;
            Left = left;
            Right = right;
        }
    }
}