using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Operators.Comparison
{
    public class ASTIsNotEqualOperator : ASTBinary
    {
        public ASTIsNotEqualOperator(TokenItem token, ASTNode left, ASTNode right) : base(token, left, right)
        {
        }
    }
}