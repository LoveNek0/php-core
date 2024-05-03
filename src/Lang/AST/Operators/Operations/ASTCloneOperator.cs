using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Operators.Operations
{
    public class ASTCloneOperator : ASTUnary
    {
        public ASTCloneOperator(TokenItem token, ASTNode operand) : base(token, operand)
        {
        }
    }
}