using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Operators.Binary.Comparsion
{
    public class ASTIsSpaceshipOperator : ASTBinary
    {
        public ASTIsSpaceshipOperator(TokenItem token, ASTNode left, ASTNode right) : base(token, left, right)
        {
        }
    }
}