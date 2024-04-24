using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Operators.Binary
{
    public class ASTNullsafeObjectAccessOperator : ASTBinary
    {
        public ASTNullsafeObjectAccessOperator(TokenItem token, ASTNode left = null, ASTNode right = null) : base(token, left, right)
        {
        }
    }
}