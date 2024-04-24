using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Operators.Operations
{
    public class ASTPrintOperator : ASTUnary
    {
        public ASTPrintOperator(TokenItem token, ASTNode operand) : base(token, operand)
        {
        }
    }
}