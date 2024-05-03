using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Operators.Logical
{
    public class ASTNotOperator : ASTUnary
    {
        public ASTNotOperator(TokenItem token, ASTNode operand) : base(token, operand)
        {
        }
    }
}