using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Operators.Binary.Comparsion
{
    public class ASTIsGreaterOrEqualOperator : ASTBinary
    {
        public ASTIsGreaterOrEqualOperator(TokenItem token, ASTNode left = null, ASTNode right = null) : base(token, left, right)
        {
        }
    }
}