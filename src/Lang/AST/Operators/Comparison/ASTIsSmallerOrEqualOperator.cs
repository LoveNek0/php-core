using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Operators.Comparison
{
    public class ASTIsSmallerOrEqualOperator : ASTBinary
    {
        public ASTIsSmallerOrEqualOperator(TokenItem token, ASTNode left = null, ASTNode right = null) : base(token, left, right)
        {
        }
    }
}