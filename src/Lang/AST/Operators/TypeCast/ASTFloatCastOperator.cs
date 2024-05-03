using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Operators.TypeCast
{
    public class ASTFloatCastOperator : ASTUnary
    {
        public ASTFloatCastOperator(TokenItem token, ASTNode operand) : base(token, operand)
        {
        }
    }
}