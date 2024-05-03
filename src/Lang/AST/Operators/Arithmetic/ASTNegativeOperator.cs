using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Operators.Arithmetic
{
    public class ASTNegativeOperator : ASTUnary
    {
        public ASTNegativeOperator(TokenItem token, ASTNode operand) : base(token, operand)
        {
        }
    }
}