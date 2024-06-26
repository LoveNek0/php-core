using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Operators.TypeCast
{
    public class ASTIntegerCastOperator : ASTUnary
    {
        public ASTIntegerCastOperator(TokenItem token, ASTNode operand) : base(token, operand)
        {
        }
    }
}