using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Operators.Binary.Bitwise
{
    public class ASTBitShiftRightOperator : ASTBinary
    {
        public ASTBitShiftRightOperator(TokenItem token, ASTNode left, ASTNode right) : base(token, left, right)
        {
        }
    }
}