using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Operators.Operations
{
    public class ASTReturnOperator : ASTUnary
    {
        public ASTReturnOperator(TokenItem token, ASTNode operand) : base(token, operand)
        {
        }
    }
}