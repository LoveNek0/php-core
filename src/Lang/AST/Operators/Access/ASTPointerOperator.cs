using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Operators.Access
{
    public class ASTPointerOperator : ASTUnary
    {
        public ASTPointerOperator(TokenItem token, ASTNode operand) : base(token, operand)
        {
        }
        public override string ToString() => $"&{Operand}";
    }
}