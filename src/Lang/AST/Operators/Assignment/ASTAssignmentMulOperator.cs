using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Operators.Assignment
{
    public class ASTAssignmentMulOperator : ASTBinary
    {
        public ASTAssignmentMulOperator(TokenItem token, ASTNode left, ASTNode right) : base(token, left, right)
        {
        }
    }
}