using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Operators.Binary
{
    public class ASTInstanceOfOperator : ASTBinary
    {
        public ASTInstanceOfOperator(TokenItem token, ASTNode left = null, ASTNode right = null) : base(token, left, right)
        {
        }
    }
}