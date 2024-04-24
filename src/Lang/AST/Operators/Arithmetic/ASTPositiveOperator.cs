using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Operators.Unary
{
    public class ASTPositiveOperator : ASTUnary
    {
        public ASTPositiveOperator(TokenItem token, ASTNode operand) : base(token, operand)
        {
        }
    }
}