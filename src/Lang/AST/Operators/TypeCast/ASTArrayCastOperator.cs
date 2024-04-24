using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Operators.Unary.TypeCast
{
    public class ASTArrayCastOperator : ASTUnary
    {
        public ASTArrayCastOperator(TokenItem token, ASTNode operand) : base(token, operand)
        {
        }
    }
}