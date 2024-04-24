using System.Linq;
using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Operators.Operations
{
    public class ASTUnsetOperator : ASTNode
    {
        public readonly ASTNode[] Variables;
        
        internal ASTUnsetOperator(TokenItem token, ASTNode[] variables) : base(token)
        {
            Variables = variables;
        }

        public override string ToString() => $"unset({string.Join(", ", Variables.Select(a => a.ToString()))})";
    }
}