using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Operators.Operations
{
    public class ASTExitOperator : ASTUnary
    {
        internal ASTExitOperator(TokenItem token, ASTNode operand) : base(token, operand)
        {
        }
    }
}