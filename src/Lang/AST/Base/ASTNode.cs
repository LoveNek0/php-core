using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Base
{
    public class ASTNode
    {
        public readonly TokenItem Token;

        protected ASTNode(TokenItem token) => Token = token;

        public override string ToString() => $"{Token.Data}";
    }
}
