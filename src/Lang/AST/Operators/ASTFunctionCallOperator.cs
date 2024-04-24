using System.Linq;
using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Operators
{
    public class ASTFunctionCallOperator : ASTNode
    {
        public readonly ASTNode Link;
        public readonly ASTNode[] Arguments;
        
        internal ASTFunctionCallOperator(TokenItem token, ASTNode link, ASTNode[] arguments) : base(token)
        {
            Link = link;
            Arguments = arguments;
        }

        public override string ToString() => $"{Link}({string.Join(", ", Arguments.Select(a => a.ToString()))})";
    }
}