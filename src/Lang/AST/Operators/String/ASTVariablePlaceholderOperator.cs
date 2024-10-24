using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Operators.String
{
    public class ASTVariablePlaceholderOperator : ASTBinary
    {
        public ASTVariablePlaceholderOperator(TokenItem token, ASTNode left, ASTNode right) : base(token, left, right)
        {
        }
    }
}