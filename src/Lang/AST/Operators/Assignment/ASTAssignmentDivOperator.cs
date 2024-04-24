using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Operators.Assignment
{
    public class ASTAssignmentDivOperator : ASTBinary
    {
        public ASTAssignmentDivOperator(TokenItem token, ASTNode left, ASTNode right) : base(token, left, right)
        {
        }
    }
}