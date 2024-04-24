using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Operators.Operations
{
    public class ASTEchoOperator : ASTUnary
    {
        public ASTEchoOperator(TokenItem token, ASTNode operand) : base(token, operand)
        {
        }
    }
}