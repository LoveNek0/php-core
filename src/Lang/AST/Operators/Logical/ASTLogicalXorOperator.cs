using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Operators.Binary.Logical
{
    public class ASTLogicalXorOperator : ASTBinary
    {
        public ASTLogicalXorOperator(TokenItem token, ASTNode left, ASTNode right) : base(token, left, right)
        {
        }
    }
}