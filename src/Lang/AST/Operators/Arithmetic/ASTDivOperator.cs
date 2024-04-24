using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Operators.Binary
{
    public class ASTDivOperator : ASTBinary
    {
        public ASTDivOperator(TokenItem token, ASTNode left = null, ASTNode right = null) : base(token, left, right)
        {
        }
    }
}