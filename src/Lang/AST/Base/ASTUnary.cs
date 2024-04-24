using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Base
{
    public abstract class ASTUnary : ASTNode
    {
        public readonly ASTNode Operand;

        internal ASTUnary(TokenItem token, ASTNode operand) : base(token)
        {
            Operand = operand;
        }
    }
}
