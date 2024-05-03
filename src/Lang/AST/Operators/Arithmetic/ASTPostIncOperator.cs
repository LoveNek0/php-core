using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Operators.Arithmetic
{
    public class ASTPostIncOperator : ASTUnary
    {
        public ASTPostIncOperator(TokenItem token, ASTNode operand) : base(token, operand)
        {
        }

        public override string ToString() => $"{Operand} {Token.Data}";
    }
}