using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Operators.Binary
{
    public class ASTAddOperator : ASTBinary
    {
        internal ASTAddOperator(TokenItem token, ASTNode left = null, ASTNode right = null) : base(token, left, right)
        {
        }
    }
}