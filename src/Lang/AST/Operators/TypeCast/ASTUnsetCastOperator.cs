using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Operators.TypeCast
{
    public class ASTUnsetCastOperator : ASTUnary
    {
        public ASTUnsetCastOperator(TokenItem token, ASTNode operand) : base(token, operand)
        {
        }
    }
}